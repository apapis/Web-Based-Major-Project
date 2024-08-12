using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Services
{
    public class ProductService
    {
        private readonly RestaurantContext _dbContext;
        private readonly MealService _mealService;

        public ProductService(RestaurantContext dbContext, MealService mealService)
        {
            _dbContext = dbContext;
            _mealService = mealService;
        }

        public async Task<Product> CreateProductAsync(Product product, IEnumerable<int> allergenIds)
        {
            product.Allergens = await _dbContext.Allergens.Where(a => allergenIds.Contains(a.Id)).ToListAsync();
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.Include(p => p.Allergens).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Include(p => p.Allergens).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProductAsync(Product product, ProductDTO updatedProductDto)
        {
            product.Name = updatedProductDto.Name;
            product.Store = updatedProductDto.Store;
            product.Quantity = updatedProductDto.Quantity;
            product.Price = updatedProductDto.Price;
            product.PricePerUnit = updatedProductDto.PricePerUnit;
            product.Unit = updatedProductDto.Unit;

            var currentAllergens = product.Allergens.ToList();
            var newAllergens = await _dbContext.Allergens
                .Where(a => updatedProductDto.AllergenIds.Contains(a.Id))
                .ToListAsync();

            foreach (var allergen in currentAllergens)
            {
                if (!updatedProductDto.AllergenIds.Contains(allergen.Id))
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
            await _dbContext.SaveChangesAsync();

            // Update related meals
            await UpdateRelatedMealsAsync(product.Id);
        }

        public async Task DeleteProductAsync(Product product)
        {
            var mealIdsToUpdate = await _dbContext.MealProducts
                .Where(mp => mp.ProductId == product.Id)
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
                    var mealProduct = meal.Ingredients.Products.FirstOrDefault(mp => mp.ProductId == product.Id);
                    if (mealProduct != null)
                    {
                        meal.Ingredients.Products.Remove(mealProduct);
                        _dbContext.MealProducts.Remove(mealProduct);
                    }

                    _mealService.AggregateAllergens(meal);
                    _mealService.UpdateMealAfterProductChange(meal);

                    await _dbContext.SaveChangesAsync();
                }
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateRelatedMealsAsync(int productId)
        {
            var mealIdsToUpdate = await _dbContext.MealProducts
                .Where(mp => mp.ProductId == productId)
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
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
