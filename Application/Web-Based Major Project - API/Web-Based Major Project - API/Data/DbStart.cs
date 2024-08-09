using Microsoft.AspNetCore.Identity;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Data
{
    public class DbStart
    {
        public static async Task Initialize(RestaurantContext context, UserManager<ApplicationUser> userManager)
        {

            if (!context.Users.Any())
            {
                var email = "user@example.com";
                var password = "Password123!";

                var user = new ApplicationUser { UserName = email, Email = email };
                await userManager.CreateAsync(user, password);
            }

            if (context.Menus.Any()) return;

            var menus = new List<Menu>();
            {
                new Menu { Id = 1, DayOfWeek = DayOfWeek.Monday, IsRestaurantOpen = true };
                new Menu { Id = 2, DayOfWeek = DayOfWeek.Tuesday, IsRestaurantOpen = true };
                new Menu { Id = 3, DayOfWeek = DayOfWeek.Wednesday, IsRestaurantOpen = true };
                new Menu { Id = 4, DayOfWeek = DayOfWeek.Thursday, IsRestaurantOpen = true };
                new Menu { Id = 5, DayOfWeek = DayOfWeek.Friday, IsRestaurantOpen = true };
                new Menu { Id = 6, DayOfWeek = DayOfWeek.Saturday, IsRestaurantOpen = true };
                new Menu { Id = 7, DayOfWeek = DayOfWeek.Sunday, IsRestaurantOpen = true };
            }

            foreach ( var menu in menus ) 
            {
                context.Menus.Add( menu );
            }
            context.SaveChanges();
          
        }
    }
}
