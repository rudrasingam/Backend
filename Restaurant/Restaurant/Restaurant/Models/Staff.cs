namespace Restaurant.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        // Foreign key
        public int RoleId { get; set; }

        // Navigation property
        public Role Role { get; set; }
    }

}
