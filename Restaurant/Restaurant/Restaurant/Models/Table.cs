namespace Restaurant.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }

        // Navigation property
        public ICollection<Booking> Bookings { get; set; }
    }

}
