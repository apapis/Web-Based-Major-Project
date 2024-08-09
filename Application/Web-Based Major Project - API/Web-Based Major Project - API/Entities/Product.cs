namespace Web_Based_Major_Project___API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Store { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
        public float PricePerUnit { get; set; }
        public List<Allergen> Allergens { get; set; } = new List<Allergen>();

    }

    public class ProductDTO
    {
        public string Name { get; set; }
        public string Store { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
        public float PricePerUnit { get; set; }
        public List<int> AllergenIds { get; set; } = new List<int>();
    }
}
