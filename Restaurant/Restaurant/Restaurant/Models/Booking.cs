namespace Restaurant.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int TableId { get; set; }
        public int GuestId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public Table Table { get; set; }
        public Guest Guest { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }

}
