using Microsoft.EntityFrameworkCore;

namespace GripFoodExam.Entities
{
    public class GripFoodDbContext : DbContext
    {
        public GripFoodDbContext(DbContextOptions<GripFoodDbContext> options) : base(options) { }

      

        public DbSet<User> Users => Set<User>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<FoodItemGridItem> FoodItems => Set<FoodItemGridItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartDetail> CartDetails => Set<CartDetail>();

    }
}
