namespace Restaurant.Models
{
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }  // Added for clarity based on start time and duration

        // Navigation property
        public ICollection<Booking> Bookings { get; set; }
    }

}
