using System.ComponentModel.DataAnnotations.Schema;
using ventasG.Models;

namespace ventasG.Models
{
    public class OrderDetails
    {
        public int id { get; set; }

        public int Productid { get; set; }

        public int Orderid { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}

