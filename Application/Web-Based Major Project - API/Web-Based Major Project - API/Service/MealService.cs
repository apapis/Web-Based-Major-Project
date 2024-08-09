using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Based_Major_Project___API.Entities;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;
using Web_Based_Major_Project___API.DTO;

public class MealService
{
    private readonly RestaurantContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MealService(RestaurantContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
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

    public void AggregateAllergens(Meal meal)
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
    }

    public List<string> GetAllergensForMeal(Meal meal)
    {
        return meal.Allergens.Allergens.Select(a => a.Name).ToList();
    }

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

    public MealDto MapMealToDto(Meal meal)
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
                    PricePerUnit = p.Product.PricePerUnit, // Zmieniona nazwa
                    Unit = p.Product.Unit, // Nowe pole
                    Allergens = p.Product.Allergens.Select(a => a.Name).ToList()
                }).ToList()
            },
            Allergens = meal.Allergens.Allergens.Select(a => a.Name).ToList(),
            ImageUrls = meal.Images.Select(i => i.ImageUrl).ToList()
        };
    }
}
