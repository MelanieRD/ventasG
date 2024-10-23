using System.ComponentModel.DataAnnotations.Schema;

namespace ventasG.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }

        public int Companyid { get; set; }

        public Company Company { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
