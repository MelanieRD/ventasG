namespace ventasG.Models
{
    public class Order
    {
        public int id { get; set; }
        public string description { get; set; }
        public int Employee_id { get; set; }

        public int id_OrderDetail {get; set;}
        public string state { get; set; }
        public int TotalValue { get; set; }

        public ICollection<OrderDetails> orderDetails{ get; set; }
    }
}
