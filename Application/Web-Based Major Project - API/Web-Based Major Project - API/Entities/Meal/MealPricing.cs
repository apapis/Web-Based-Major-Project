using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Entities.Meal
{
    public class MealPricing
    {
        [Required(ErrorMessage = "MealImage ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MealImage ID must be a positive number.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "NumberOfPeople is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "NumberOfPeople must be a positive number.")]
        public int NumberOfPeople { get; set; }
        public float CostOfAllIngredients { get; set; }
        public float CostOfMakeIt { get; set; }
        public float ProposedPrice { get; set; }
        
    }
}
