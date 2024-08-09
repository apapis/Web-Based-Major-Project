using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.DTO;

namespace Web_Based_Major_Project___API.Controllers
{
    [Authorize]
    [Route("api/meals")]
    public class MealController : ControllerBase
    {
        private readonly RestaurantContext _dbContext;
        private readonly MealService _mealService;

        public MealController(RestaurantContext dbContext, MealService mealService)
        {
            _dbContext = dbContext;
            _mealService = mealService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CreateMeal([FromForm] CreateMealDto mealDto)
        {
            if (mealDto.NumberOfPeople <= 0)
            {
                return BadRequest("NumberOfPeople must be greater than 0.");
            }

            if (string.IsNullOrEmpty(mealDto.Name))
            {
                return BadRequest("Meal name is required.");
            }

            var products = JsonConvert.DeserializeObject<List<CreateMealProductDto>>(mealDto.Products);
            var costs = JsonConvert.DeserializeObject<List<CreateMealCostDto>>(mealDto.Costs);

            var meal = new Meal
            {
                Name = mealDto.Name,
                Description = mealDto.Description,
                NumberOfPeople = mealDto.NumberOfPeople
            };

            _dbContext.Meals.Add(meal);
            await _dbContext.SaveChangesAsync(); // Save the Meal entity first

            meal.Pricing = new MealPricing
            {
                Id = meal.Id, // Ensure the Ids match
                Price = mealDto.Price,
                Costs = costs.Select(c => new MealCost { Name = c.Name, Value = c.Value, MealPricingId = meal.Id }).ToList()
            };

            meal.Ingredients = new MealIngredients
            {
                Id = meal.Id, // Ensure the Ids match
                Products = products.Select(p => new MealProduct { ProductId = p.ProductId, Quantity = p.Quantity, MealIngredientsId = meal.Id }).ToList()
            };

            meal.Allergens = new MealAllergens
            {
                Id = meal.Id,
                MealId = meal.Id
            };

            _dbContext.MealPricings.Add(meal.Pricing);
            _dbContext.MealIngredients.Add(meal.Ingredients);
            _dbContext.MealAllergens.Add(meal.Allergens);

            if (mealDto.Images != null && mealDto.Images.Count > 0)
            {
                foreach (var image in mealDto.Images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var imageUrl = await _mealService.SaveImageAsync(image);
                        var mealImage = new MealImage { ImageUrl = imageUrl, MealId = meal.Id };
                        _dbContext.MealImages.Add(mealImage);
                    }
                }
            }

            await _dbContext.SaveChangesAsync(); // Save all related entities

            _mealService.AggregateAllergens(meal);
            _mealService.UpdateMealAfterProductChange(meal);

            await _dbContext.SaveChangesAsync();

            var createdMeal = _mealService.MapMealToDto(meal);

            return CreatedAtAction(nameof(GetMealById), new { id = meal.Id }, createdMeal);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeal(int id, [FromForm] UpdateMealDto mealDto)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Pricing)
                    .ThenInclude(p => p.Costs)
                .Include(m => m.Ingredients)
                    .ThenInclude(i => i.Products)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Allergens)
                .Include(m => m.Images)
                .Include(m => m.Allergens)
                    .ThenInclude(a => a.Allergens)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            var products = JsonConvert.DeserializeObject<List<CreateMealProductDto>>(mealDto.Products);
            var costs = JsonConvert.DeserializeObject<List<CreateMealCostDto>>(mealDto.Costs);
            var existingImageUrls = JsonConvert.DeserializeObject<List<string>>(mealDto.ExistingImageUrls);

            meal.Name = mealDto.Name;
            meal.Description = mealDto.Description;
            meal.NumberOfPeople = mealDto.NumberOfPeople;
            meal.Pricing.Price = mealDto.Price;

            _dbContext.MealProducts.RemoveRange(meal.Ingredients.Products);
            meal.Ingredients.Products = products.Select(p => new MealProduct { ProductId = p.ProductId, Quantity = p.Quantity, MealIngredientsId = meal.Ingredients.Id }).ToList();

            _dbContext.MealCosts.RemoveRange(meal.Pricing.Costs);
            meal.Pricing.Costs = costs.Select(c => new MealCost { Name = c.Name, Value = c.Value, MealPricingId = meal.Pricing.Id }).ToList();

            if (mealDto.Images != null && mealDto.Images.Count > 0)
            {
                var imagesToRemove = meal.Images.Where(i => !existingImageUrls.Contains(i.ImageUrl)).ToList();
                _dbContext.MealImages.RemoveRange(imagesToRemove);

                foreach (var image in mealDto.Images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var imageUrl = await _mealService.SaveImageAsync(image);
                        var mealImage = new MealImage { ImageUrl = imageUrl, MealId = meal.Id };
                        _dbContext.MealImages.Add(mealImage);
                    }
                }
            }
            else
            {
                var imagesToRemove = meal.Images.Where(i => !existingImageUrls.Contains(i.ImageUrl)).ToList();
                _dbContext.MealImages.RemoveRange(imagesToRemove);
            }

            await _dbContext.SaveChangesAsync();

            // Update allergens after the meal and its products are saved
            _mealService.AggregateAllergens(meal);
            _mealService.UpdateMealAfterProductChange(meal);

            await _dbContext.SaveChangesAsync();

            var updatedMeal = _mealService.MapMealToDto(meal);

            return Ok(updatedMeal);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetAllMeals()
        {
            var meals = await _dbContext.Meals
                .Include(m => m.Pricing)
                    .ThenInclude(p => p.Costs)
                .Include(m => m.Ingredients)
                    .ThenInclude(i => i.Products)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Allergens)
                .Include(m => m.Images)
                .Include(m => m.Allergens)
                    .ThenInclude(a => a.Allergens)
                .ToListAsync();

            var mealDtos = meals.Select(m => _mealService.MapMealToDto(m)).ToList();

            return Ok(mealDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMealById(int id)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Pricing)
                    .ThenInclude(p => p.Costs)
                .Include(m => m.Ingredients)
                    .ThenInclude(i => i.Products)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Allergens)
                .Include(m => m.Images)
                .Include(m => m.Allergens)
                    .ThenInclude(a => a.Allergens)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            var mealDto = _mealService.MapMealToDto(meal);

            return Ok(mealDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(int id)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Pricing)
                    .ThenInclude(p => p.Costs)
                .Include(m => m.Ingredients)
                    .ThenInclude(i => i.Products)
                .Include(m => m.Images)
                .Include(m => m.Allergens)
                    .ThenInclude(a => a.Allergens)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            _dbContext.Allergens.RemoveRange(meal.Allergens.Allergens);
            _dbContext.MealProducts.RemoveRange(meal.Ingredients.Products);
            _dbContext.MealCosts.RemoveRange(meal.Pricing.Costs);
            _dbContext.MealImages.RemoveRange(meal.Images);
            _dbContext.MealAllergens.Remove(meal.Allergens);
            _dbContext.MealPricings.Remove(meal.Pricing);
            _dbContext.MealIngredients.Remove(meal.Ingredients);
            _dbContext.Meals.Remove(meal);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}