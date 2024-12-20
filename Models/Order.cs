namespace RestoranBoshqaruvi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Dish { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int WaiterId { get; set; } // Ofitsiant ID
    }
}
