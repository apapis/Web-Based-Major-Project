using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Major_Project___API.Entities;
using System.Threading.Tasks;

namespace Web_Based_Major_Project___API.Controllers
{
    [Authorize]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly RestaurantContext _dbContext;
        private readonly MealService _mealService;

        public ProductController(RestaurantContext dbContext, MealService mealService)
        {
            _dbContext = dbContext;
            _mealService = mealService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Store = productDto.Store,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                PricePerUnit = productDto.PricePerUnit,
                Unit = productDto.Unit,
                Allergens = await _dbContext.Allergens.Where(a => productDto.AllergenIds.Contains(a.Id)).ToListAsync(),
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _dbContext.Products.Include(p => p.Allergens).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _dbContext.Products.Include(p => p.Allergens).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDTO updatedProduct)
        {
            var product = await _dbContext.Products
                .Include(p => p.Allergens)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Store = updatedProduct.Store;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;
            product.PricePerUnit = updatedProduct.PricePerUnit;
            product.Unit = updatedProduct.Unit;

            // Update allergens
            var currentAllergens = product.Allergens.ToList();
            var newAllergens = await _dbContext.Allergens
                .Where(a => updatedProduct.AllergenIds.Contains(a.Id))
                .ToListAsync();

            foreach (var allergen in currentAllergens)
            {
                if (!updatedProduct.AllergenIds.Contains(allergen.Id))
                {
                    product.Allergens.Remove(allergen);
                }
            }

            foreach (var allergen in newAllergens)
            {
                if (!product.Allergens.Any(a => a.Id == allergen.Id))
                {
                    product.Allergens.Add(allergen);
                }
            }

            _dbContext.Products.Update(product);

            // Save changes to the product first
            await _dbContext.SaveChangesAsync();

            // Update related meals
            var mealIdsToUpdate = await _dbContext.MealProducts
                .Where(mp => mp.ProductId == id)
                .Select(mp => mp.MealIngredientsId)
                .Distinct()
                .ToListAsync();

            foreach (var mealId in mealIdsToUpdate)
            {
                var meal = await _dbContext.Meals
                    .Include(m => m.Pricing)
                        .ThenInclude(p => p.Costs)
                    .Include(m => m.Ingredients)
                        .ThenInclude(i => i.Products)
                            .ThenInclude(mp => mp.Product)
                                .ThenInclude(p => p.Allergens)
                    .Include(m => m.Allergens)
                        .ThenInclude(a => a.Allergens)
                    .FirstOrDefaultAsync(m => m.Id == mealId);

                if (meal != null)
                {
                    _mealService.AggregateAllergens(meal);
                    _mealService.UpdateMealAfterProductChange(meal);

                    // Save changes to the meal
                    await _dbContext.SaveChangesAsync();
                }
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.Allergens)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Find all meals that contain this product
            var mealIdsToUpdate = await _dbContext.MealProducts
                .Where(mp => mp.ProductId == id)
                .Select(mp => mp.MealIngredientsId)
                .Distinct()
                .ToListAsync();

            foreach (var mealId in mealIdsToUpdate)
            {
                var meal = await _dbContext.Meals
                    .Include(m => m.Pricing)
                        .ThenInclude(p => p.Costs)
                    .Include(m => m.Ingredients)
                        .ThenInclude(i => i.Products)
                            .ThenInclude(mp => mp.Product)
                                .ThenInclude(p => p.Allergens)
                    .Include(m => m.Allergens)
                        .ThenInclude(a => a.Allergens)
                    .FirstOrDefaultAsync(m => m.Id == mealId);

                if (meal != null)
                {
                    // Remove the product from the meal
                    var mealProduct = meal.Ingredients.Products.FirstOrDefault(mp => mp.ProductId == id);
                    if (mealProduct != null)
                    {
                        meal.Ingredients.Products.Remove(mealProduct);
                        _dbContext.MealProducts.Remove(mealProduct);
                    }

                    // Update the meal
                    _mealService.AggregateAllergens(meal);
                    _mealService.UpdateMealAfterProductChange(meal);

                    // Save changes to the meal
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Remove the product
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return Ok(id);
        }
    }
}
