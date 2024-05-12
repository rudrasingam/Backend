namespace Restaurant.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }

        // Navigation property
        public Order Order { get; set; }
    }

}
