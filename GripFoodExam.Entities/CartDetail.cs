namespace GripFoodExam.Entities
{
    public class CartDetail
    {
        public string Id { get; set; } = "";
        public string CartId { get; set; } = "";
        public Cart Cart { get; set; } = null!;
        public string FoodItemId { get; set; } = "";
        public FoodItemGridItem FoodItem { get; set; } = null!;
        public int Quantity { get; set; }
    }
}