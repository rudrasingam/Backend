namespace Restaurant.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // Navigation properties
        public ICollection<Staff> Staffs { get; set; }
    }

}
