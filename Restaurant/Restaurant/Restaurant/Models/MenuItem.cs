namespace Restaurant.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TimeToCook { get; set; }
        public bool SoldOut { get; set; }

        // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
