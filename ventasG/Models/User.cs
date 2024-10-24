namespace ventasG.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username{ get; set; }

        public string PasswordHash { get; set; }
    }

   
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
