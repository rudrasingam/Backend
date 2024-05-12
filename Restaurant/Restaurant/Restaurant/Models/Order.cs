namespace Restaurant.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }  // Correct property name
        public decimal TotalAmount { get; set; }
        public int GuestId { get; set; }

        // Navigation properties
        public Guest Guest { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }



}
