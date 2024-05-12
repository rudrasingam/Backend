namespace Restaurant.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation property
        public ICollection<Booking> Bookings { get; set; }
    }

}
