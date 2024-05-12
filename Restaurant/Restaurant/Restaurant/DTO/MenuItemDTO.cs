namespace Restaurant.DTO
{
    public class MenuItemDTO
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TimeToCook { get; set; }
        public bool SoldOut { get; set; }
    }

}
