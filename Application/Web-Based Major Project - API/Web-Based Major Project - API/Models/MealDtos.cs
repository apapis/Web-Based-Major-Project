namespace Web_Based_Major_Project___API.DTO
{
    public class CreateMealDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPeople { get; set; }
        public float Price { get; set; }
        public string Products { get; set; }
        public string Costs { get; set; }
        public List<IFormFile> Images { get; set; }
    }

    public class CreateMealCostDto
    {
        public string Name { get; set; }
        public float Value { get; set; }
    }

    public class CreateMealProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPeople { get; set; }
        public MealPricingDto Pricing { get; set; }
        public MealIngredientsDto Ingredients { get; set; }
        public List<string> Allergens { get; set; }
        public List<string> ImageUrls { get; set; }
    }

    public class MealPricingDto
    {
        public float ProposedPrice { get; set; }
        public float Price { get; set; }
        public List<MealCostDto> Costs { get; set; }
        public float CostOfAllIngredients { get; set; }
        public float CostOfMakeIt { get; set; }
    }

    public class MealIngredientsDto
    {
        public List<MealProductDto> Products { get; set; }
    }

    public class MealCostDto
    {
        public string Name { get; set; }
        public float Value { get; set; }
    }

    public class MealProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public float PricePerUnit { get; set; }
        public string Unit { get; set; }
        public List<string> Allergens { get; set; }
    }

    public class UpdateMealDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPeople { get; set; }
        public float Price { get; set; }
        public string Products { get; set; }
        public string Costs { get; set; }
        public List<IFormFile> Images { get; set; }
        public string ExistingImageUrls { get; set; }
    }
}