using System.ComponentModel.DataAnnotations;

namespace Web_Based_Major_Project___API.Entities
{
    public class Unit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Store name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Allergen name must be between 1 and 50 characters.")]
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
