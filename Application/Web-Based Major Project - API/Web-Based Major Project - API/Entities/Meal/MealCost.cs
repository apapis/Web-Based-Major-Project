using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Entities.Meal
{
    public class MealCost
    {
        [Required(ErrorMessage = "MealCost ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MealCost ID must be a positive number.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "MealCost name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "MealCost name must be between 1 and 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Value is required.")]
        [Range(1, float.MaxValue, ErrorMessage = "Value must be a positive number.")]
        public float Value { get; set; }
    }
}
