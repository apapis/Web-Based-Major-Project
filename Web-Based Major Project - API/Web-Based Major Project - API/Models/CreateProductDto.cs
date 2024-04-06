namespace Web_Based_Major_Project___API.Models
{
    public class CreateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Store { get; set; }
        public int weight { get; set; }
        public float Price { get; set; }
        public float PricePerGram { get; set; }
    }
}
