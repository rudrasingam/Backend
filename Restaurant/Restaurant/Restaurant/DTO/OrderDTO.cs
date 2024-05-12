namespace Restaurant.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }  // Correct property name
        public decimal TotalAmount { get; set; }
    }

}
