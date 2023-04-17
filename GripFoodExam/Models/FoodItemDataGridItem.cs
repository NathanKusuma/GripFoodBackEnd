namespace GripFoodExam.Models
{
    public class FoodItemDataGridItem
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string RestaurantId { get; set; } = "";
        public decimal Price { get; set; }
        public int TotalItem { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
