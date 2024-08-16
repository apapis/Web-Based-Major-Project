using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Entities
{
    public class Allergen
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Allergen name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Allergen name must be between 1 and 100 characters.")]
        public string Name { get; set; }
    }
}
