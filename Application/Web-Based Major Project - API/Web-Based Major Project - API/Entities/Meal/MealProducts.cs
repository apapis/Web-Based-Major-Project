using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Entities.Meal
{
    public class MealProduct
    {
        [Required(ErrorMessage = "MealProduct ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MealProduct ID must be a positive number.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be a positive number.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public float Quantity { get; set; }
    }
}
