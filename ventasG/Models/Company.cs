using Microsoft.EntityFrameworkCore;
namespace ventasG.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }

        public ICollection<employee> Employees { get; set; }
        public ICollection<Product> Products { get; set; }
    }


    public class CreatePutCompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}


