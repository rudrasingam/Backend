namespace Restaurant.DTO
{
    public class StaffDTO
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; } // Optionally include RoleName if needed
    }

}
