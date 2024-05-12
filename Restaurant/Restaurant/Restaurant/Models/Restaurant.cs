using System.Data;

namespace Restaurant.Models
{
    public class Restaurant
    {
        public string RestaurantName { get; set; }

        // Navigation properties
        public ICollection<Role> Roles { get; set; }
    }

}
