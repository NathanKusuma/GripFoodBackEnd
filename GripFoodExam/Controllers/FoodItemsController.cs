using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GripFoodExam.Entities;
using GripFoodExam.Models;

namespace GripFoodExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly GripFoodDbContext _context;

        public FoodItemsController(GripFoodDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodItems
        [HttpGet]
        public async Task<ActionResult<List<FoodItemDataGridItem>>> GetFoodItems()
        {
          if (_context.FoodItems == null)
          {
              return NotFound();
          }
            return await _context.FoodItems.AsNoTracking().Select(Q => new FoodItemDataGridItem
            {
                Id = Q.Id,
                Name = Q.Name,
                Price = Q.Price,
                RestaurantId=Q.RestaurantId,
                TotalItem = Q.CartDetails.Sum(cd=>cd.Quantity),
                TotalPrice = Q.Price * Q.CartDetails.Sum(cd => cd.Quantity)
            }).ToListAsync();
        }

        // GET: api/FoodItems/5
        [HttpGet("{id}",Name ="GetFoodItemDetail")]
        public async Task<ActionResult<FoodItemDataGridItem>> GetFoodItem(string id)
        {
          if (_context.FoodItems == null)
          {
              return NotFound();
          }
            var foodItem = await _context.FoodItems.AsNoTracking().Select(Q => new FoodItemDataGridItem
            {
                Id = Q.Id,
                Name = Q.Name,
                Price = Q.Price,
                RestaurantId = Q.RestaurantId,
                TotalItem = Q.CartDetails.Sum(cd => cd.Quantity),
                TotalPrice = Q.Price * Q.CartDetails.Sum(cd => cd.Quantity)
            }).FirstOrDefaultAsync();

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // PUT: api/FoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(string id, FoodItemGridItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodItemGridItem>> PostFoodItem(FoodItemCreateModel foodItem)
        {
          if (_context.FoodItems == null)
          {
              return Problem("Entity set 'GripFoodDbContext.FoodItems'  is null.");
          }
            var insert = new FoodItemGridItem
            {
                Id = Ulid.NewUlid().ToString(),
                Name=foodItem.Name,
                Price=foodItem.Price,
                RestaurantId=foodItem.RestaurantId
            };

            _context.FoodItems.Add(insert);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodItemExists(insert.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return insert;
        }

        // DELETE: api/FoodItems/5
        [HttpDelete("{id}",Name ="DeleteFoodItem")]
        public async Task<IActionResult> DeleteFoodItem(string id)
        {
            if (_context.FoodItems == null)
            {
                return NotFound();
            }
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(string id)
        {
            return (_context.FoodItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
