using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.DTO;
using Web_Based_Major_Project___API.Entities;

public class MealService
{
    private readonly RestaurantContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MealService(RestaurantContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    /*public async Task<MealDto> CreateMealAsync(CreateMealDto mealDto)
    {
        if (mealDto.NumberOfPeople <= 0)
        {
            throw new ArgumentException("NumberOfPeople must be greater than 0.");
        }

        if (string.IsNullOrEmpty(mealDto.Name))
        {
            throw new ArgumentException("Meal name is required.");
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
                    var imageUrl = await SaveImageAsync(image);
                    var mealImage = new MealImage { ImageUrl = imageUrl, MealId = meal.Id };
                    _dbContext.MealImages.Add(mealImage);
                }
            }
        }

        await _dbContext.SaveChangesAsync(); // Save all related entities

        AggregateAllergens(meal);
        UpdateMealAfterProductChange(meal);

        await _dbContext.SaveChangesAsync();

        return MapMealToDto(meal);
    }

    public async Task<MealDto> UpdateMealAsync(int id, UpdateMealDto mealDto)
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
            return null;
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
                    var imageUrl = await SaveImageAsync(image);
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

        AggregateAllergens(meal);
        UpdateMealAfterProductChange(meal);

        await _dbContext.SaveChangesAsync();

        return MapMealToDto(meal);
    }

    public async Task<IEnumerable<MealDto>> GetAllMealsAsync()
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

        return meals.Select(m => MapMealToDto(m)).ToList();
    }

    public async Task<MealDto> GetMealByIdAsync(int id)
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
            return null;
        }

        return MapMealToDto(meal);
    }*/

    public async Task<bool> DeleteMealAsync(int id)
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
            return false;
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

        return true;
    }

    public void UpdateMealAfterProductChange(Meal meal)
    {
        meal.Pricing.CostOfAllIngredients = CalculateCostOfAllIngredients(meal);
        meal.Pricing.CostOfMakeIt = meal.Pricing.CostOfAllIngredients + meal.Pricing.Costs.Sum(mc => mc.Value);
        meal.Pricing.ProposedPrice = meal.Pricing.CostOfMakeIt / meal.NumberOfPeople;
    }

    public float CalculateCostOfAllIngredients(Meal meal)
    {
        if (meal.Ingredients.Products == null) return 0;
        return meal.Ingredients.Products.Sum(mp => mp.Quantity * mp.Product.PricePerUnit);
    }

    /*public void AggregateAllergens(Meal meal)
    {
        var uniqueAllergens = new HashSet<Allergen>();

        foreach (var mealProduct in meal.Ingredients.Products)
        {
            var product = _dbContext.Products
                .Include(p => p.Allergens)
                .FirstOrDefault(p => p.Id == mealProduct.ProductId);

            if (product != null)
            {
                foreach (var allergen in product.Allergens)
                {
                    uniqueAllergens.Add(allergen);
                }
            }
        }

        meal.Allergens.Allergens = uniqueAllergens.ToList();
        _dbContext.MealAllergens.Update(meal.Allergens);
    }*/

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        var folderPath = Path.Combine("wwwroot", "uploads", "images");
        var folderFullPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

        if (!Directory.Exists(folderFullPath))
        {
            Directory.CreateDirectory(folderFullPath);
        }

        var fileName = $"{Guid.NewGuid()}_{image.FileName}";
        var filePath = Path.Combine(folderFullPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        var request = _httpContextAccessor.HttpContext.Request;
        //var baseUrl = $"{request.Scheme}://{request.Host}";
        var baseUrl = $"{request.Scheme}://192.168.18.133:5000";
        return $"{baseUrl}/uploads/images/{fileName}";
    }

    /*public MealDto MapMealToDto(Meal meal)
    {
        return new MealDto
        {
            Id = meal.Id,
            Name = meal.Name,
            Description = meal.Description,
            NumberOfPeople = meal.NumberOfPeople,
            Pricing = new MealPricingDto
            {
                ProposedPrice = meal.Pricing.ProposedPrice,
                Price = meal.Pricing.Price,
                Costs = meal.Pricing.Costs.Select(c => new MealCostDto
                {
                    Name = c.Name,
                    Value = c.Value
                }).ToList(),
                CostOfAllIngredients = meal.Pricing.CostOfAllIngredients,
                CostOfMakeIt = meal.Pricing.CostOfMakeIt
            },
            Ingredients = new MealIngredientsDto
            {
                Products = meal.Ingredients.Products.Select(p => new MealProductDto
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    ProductName = p.Product.Name,
                    PricePerUnit = p.Product.PricePerUnit,
                    Unit = p.Product.Unit,
                    Allergens = p.Product.Allergens.Select(a => a.Name).ToList()
                }).ToList()
            },
            Allergens = meal.Allergens.Allergens.Select(a => a.Name).ToList(),
            ImageUrls = meal.Images.Select(i => i.ImageUrl).ToList()
        };
    }*/
}
