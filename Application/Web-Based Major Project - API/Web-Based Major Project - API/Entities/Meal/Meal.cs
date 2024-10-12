using System.ComponentModel.DataAnnotations;
using Web_Based_Major_Project___API.Validation;

namespace Web_Based_Major_Project___API.Entities.Meal
{
    public class Meal
    {
        [Required(ErrorMessage = "Meal ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Meal ID must be a positive number.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Meal name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Meal name must be between 1 and 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Meal description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public float Price { get; set; }
        public List<MealImage> Images { get; set; } = new List<MealImage>();
        [Required(ErrorMessage = "Meal products are required.")]
        public List<MealProduct> MealProducts { get; set; } = new List<MealProduct>();
        public MealPricing MealPricing { get; set; }
        [PositiveIntList(ErrorMessage = "All allergen IDs must be positive numbers.")]
        public List<int> MealAllergenIds { get; set; } = new List<int>();
        public List<MealCost> MealCosts { get; set; } = new List<MealCost>();
    }

}
