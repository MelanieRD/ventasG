using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ventasG.Models;

namespace ventasG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Método para autenticar al usuario y generar el token JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            // Busca al usuario en la base de datos
            var user = await _context.User_TB.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null || !VerifyPassword(login.Password, user.PasswordHash))
            {
                // Si no se encuentra el usuario o la contraseña no es válida, devuelve Unauthorized
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // Si el usuario es válido, genera el token JWT
            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        // Método para generar el token JWT
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Método para verificar la contraseña
        private bool VerifyPassword(string password, string storedHash)
        {
            // Aquí puedes utilizar un algoritmo de hashing como BCrypt para verificar la contraseña
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }


        // Método para registrar un nuevo usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            // Verificar si el nombre de usuario ya existe
            if (await _context.User_TB.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("El nombre de usuario ya está en uso >:V .");
            }

            // Encriptar la contraseña antes de guardarla
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Crear un nuevo usuario
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash
            };

            _context.User_TB.Add(user);
            await _context.SaveChangesAsync();

            return Ok("exitosamente registrado <trez ");
        }

        //// Método para hashear la contraseña
        //private string HashPassword(string password)
        //{
        //    byte[] salt = Encoding.ASCII.GetBytes("una-sal-segura"); // Sal estática para simplificar, puedes mejorarla
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));
        //    return hashed;
        //}
    }




  
}
