﻿namespace GripFoodExam.Entities
{
    public class FoodItemGridItem
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string RestaurantId { get; set; } = "";
        public Restaurant Restaurant { get; set; } = null!;
        public List<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }
}