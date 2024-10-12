namespace Web_Based_Major_Project___API.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsRestaurantOpen { get; set; }
        public List<MenuMeal> MenuMeals { get; set; } = new List<MenuMeal>();
    }

    public class MenuMeal
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int MealId { get; set; }
        /*public Meal Meal { get; set; }*/
    }
}
