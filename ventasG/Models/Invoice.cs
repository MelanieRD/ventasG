using System.ComponentModel.DataAnnotations.Schema;

namespace ventasG.Models
{
    public class Invoice
    {

        public int id { get; set; }

    
        public int Orderid { get; set; }
        public string state { get; set; }

        public DateTime delivery_date { get; set; }
     
    }



    public class InvoicePutPostDto
    {
        public int id { get; set; }

        public int Orderid { get; set; }
        public string state { get; set; }


    }


}
