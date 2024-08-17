using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Models
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Store Store { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public float Price { get; set; }
        public float PricePerUnit { get; set; }
        public List<Allergen> Allergens { get; set; } = new List<Allergen>();
    }
}
