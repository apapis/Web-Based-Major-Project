using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Web_Based_Major_Project___API.Validation;

namespace Web_Based_Major_Project___API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name must be between 1 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Store ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Store ID must be a positive number.")]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Unit ID must be a positive number.")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Price per unit is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Price per unit must be a non-negative number.")]
        public float PricePerUnit { get; set; }
        [PositiveIntList(ErrorMessage = "All allergen IDs must be positive numbers.")]
        public List<int> AllergenIds { get; set; } = new List<int>();

    }
}
