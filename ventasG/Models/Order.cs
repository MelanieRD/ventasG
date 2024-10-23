namespace ventasG.Models
{
    public class Order
    {
        public int Orderid { get; set; }
        public string description { get; set; }
        public int EmployeeId { get; set; }

        public int id_OrderDetail {get; set;}
        public string state { get; set; }
        public int TotalValue { get; set; }

        public employee Employee { get; set; }
        public ICollection<OrderDetails> OrderDetail { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }



    public class OrderCreatePutDto {

        public int Orderid { get; set; }
        public string description { get; set; }
        public int EmployeeId { get; set; }

        public int id_OrderDetail { get; set; }
        public string state { get; set; }
        public int TotalValue { get; set; }

    }
}
