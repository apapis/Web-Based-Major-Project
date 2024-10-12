using System.ComponentModel.DataAnnotations;
using Web_Based_Major_Project___API.Validation;

namespace Web_Based_Major_Project___API.DTO
{
    public class MealDTO
    {
        
        public int? Id { get; set; }
        [Required(ErrorMessage = "Meal name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Meal name must be between 1 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Meal description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public float Price { get; set; }

        public List<MealImageDTO> Images { get; set; } = new List<MealImageDTO>();

        [Required(ErrorMessage = "Meal products are required.")]
        public List<MealProductDTO> MealProducts { get; set; } = new List<MealProductDTO>();

        public MealPricingDTO MealPricing { get; set; }

        [PositiveIntList(ErrorMessage = "All allergen IDs must be positive numbers.")]
        public List<int> MealAllergenIds { get; set; } = new List<int>();

        public List<MealCostDTO> MealCosts { get; set; } = new List<MealCostDTO>();
    }

    public class MealImageDTO
    {
        [Required(ErrorMessage = "Image URL is required.")]
        public string ImageUrl { get; set; }
    }

    public class MealProductDTO
    {
        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be a positive number.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public float Quantity { get; set; }
    }

    public class MealPricingDTO
    {
        [Required(ErrorMessage = "NumberOfPeople is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "NumberOfPeople must be a positive number.")]
        public int NumberOfPeople { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "CostOfAllIngredients must be a non-negative number.")]
        public float CostOfAllIngredients { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "CostOfMakeIt must be a non-negative number.")]
        public float CostOfMakeIt { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "ProposedPrice must be a non-negative number.")]
        public float ProposedPrice { get; set; }
    }

    public class MealCostDTO
    {
        [Required(ErrorMessage = "MealCost name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "MealCost name must be between 1 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Value is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Value must be a non-negative number.")]
        public float Value { get; set; }
    }
}