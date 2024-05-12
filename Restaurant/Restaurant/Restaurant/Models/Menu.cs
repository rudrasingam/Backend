namespace Restaurant.Models
{
    public class Menu
    {
        public int MenuId { get; set; }

        // Navigation property
        public ICollection<MenuItem> MenuItems { get; set; }
    }

}
