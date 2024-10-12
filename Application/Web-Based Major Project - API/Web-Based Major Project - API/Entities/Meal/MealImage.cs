using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Entities.Meal
{
    public class MealImage
    {
        [Required(ErrorMessage = "MealImage ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MealImage ID must be a positive number.")]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
