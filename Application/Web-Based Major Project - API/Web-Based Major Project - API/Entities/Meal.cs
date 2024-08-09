namespace Web_Based_Major_Project___API.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPeople { get; set; }
        public List<MealImage> Images { get; set; } = new List<MealImage>();
        public List<MenuMeal> MenuMeals { get; set; } = new List<MenuMeal>();
        public MealPricing Pricing { get; set; }
        public MealIngredients Ingredients { get; set; }
        public MealAllergens Allergens { get; set; }
    }

    public class MealPricing
    {
        public int Id { get; set; }
        public float ProposedPrice { get; set; }
        public float Price { get; set; }
        public List<MealCost> Costs { get; set; } = new List<MealCost>();
        public float CostOfAllIngredients { get; set; }
        public float CostOfMakeIt { get; set; }
    }

    public class MealIngredients
    {
        public int Id { get; set; }
        public List<MealProduct> Products { get; set; } = new List<MealProduct>();
    }

    public class MealAllergens
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public List<Allergen> Allergens { get; set; } = new List<Allergen>();
    }


    public class MealCost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public int MealPricingId { get; set; }
        public MealPricing MealPricing { get; set; }
    }

    public class MealProduct
    {
        public int MealIngredientsId { get; set; }
        public MealIngredients MealIngredients { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class MealImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }

}
