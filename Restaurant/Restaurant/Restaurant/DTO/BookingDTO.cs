namespace Restaurant.DTO
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int TableId { get; set; }
        public int GuestId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime Date { get; set; }

        // Optional: Include details from related entities if needed for the front end
        public string TableName { get; set; }  // Example: name or identifier from the Table entity
        public string GuestName { get; set; }  // Example: Concatenated name from the Guest entity
        public string TimeSlotRange { get; set; }  // Example: Formatted time range from TimeSlot
    }
}
