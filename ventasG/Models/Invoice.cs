namespace ventasG.Models
{
    public class Invoice
    {

        public int id { get; set; }
        public int Order_id { get; set; }
        public string state { get; set; }

        public DateOnly delivery_date { get; set; }
       
    }
}
