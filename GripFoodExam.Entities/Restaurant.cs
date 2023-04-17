namespace GripFoodExam.Entities
{
    public class Restaurant
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public List<FoodItemGridItem> FoodItems { get; set; } = new List<FoodItemGridItem>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
    }
}