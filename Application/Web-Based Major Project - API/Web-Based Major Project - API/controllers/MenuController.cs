using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.DTO;

namespace Web_Based_Major_Project___API.Controllers
{
    [Authorize]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        /*private readonly RestaurantContext _dbContext;

        public MenuController(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<MenuDto> CreateMenu(CreateMenuDto createMenuDto)
        {
            if (!Enum.TryParse<DayOfWeek>(createMenuDto.DayOfWeek, out var dayOfWeek))
            {
                return BadRequest("Invalid day of week. Please provide a valid day of week (e.g., Monday, Tuesday, etc.).");
            }

            var menu = new Menu
            {
                DayOfWeek = dayOfWeek,
                IsRestaurantOpen = createMenuDto.IsRestaurantOpen
            };

            foreach (var mealId in createMenuDto.MealIds)
            {
                var meal = _dbContext.Meals.Include(m => m.Pricing).FirstOrDefault(m => m.Id == mealId);
                if (meal == null)
                {
                    return BadRequest($"Meal with ID {mealId} not found.");
                }
                menu.MenuMeals.Add(new MenuMeal { Meal = meal });
            }

            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();

            var menuDto = MapMenuToDto(menu);

            return CreatedAtAction(nameof(GetMenuForDayOfWeek), new { dayOfWeek = menu.DayOfWeek }, menuDto);
        }

        [HttpPut("{id}")]
        public ActionResult<MenuDto> UpdateMenu(int id, UpdateMenuDto updateMenuDto)
        {
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Allergens)
                            .ThenInclude(a => a.Allergens)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Images)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Ingredients)
                            .ThenInclude(i => i.Products)
                                .ThenInclude(mp => mp.Product)
                .FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            if (!Enum.TryParse<DayOfWeek>(updateMenuDto.DayOfWeek, out var dayOfWeek))
            {
                return BadRequest("Invalid day of week. Please provide a valid day of week (e.g., Monday, Tuesday, etc.).");
            }

            menu.DayOfWeek = dayOfWeek;
            menu.IsRestaurantOpen = updateMenuDto.IsRestaurantOpen;

            _dbContext.MenuMeals.RemoveRange(menu.MenuMeals);

            foreach (var mealId in updateMenuDto.MealIds)
            {
                var meal = _dbContext.Meals.Include(m => m.Pricing).FirstOrDefault(m => m.Id == mealId);
                if (meal == null)
                {
                    return BadRequest($"Meal with ID {mealId} not found.");
                }
                menu.MenuMeals.Add(new MenuMeal { Meal = meal });
            }

            _dbContext.SaveChanges();

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpGet("today")]
        [AllowAnonymous]
        public ActionResult<MenuDto> GetMenuForToday()
        {
            var today = DateTime.Today.DayOfWeek;
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Allergens)
                            .ThenInclude(a => a.Allergens)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Images)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Ingredients)
                            .ThenInclude(i => i.Products)
                                .ThenInclude(mp => mp.Product)
                .FirstOrDefault(m => m.DayOfWeek == today);

            if (menu == null)
            {
                return NotFound();
            }

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpGet("{dayOfWeek}")]
        public ActionResult<MenuDto> GetMenuForDayOfWeek(DayOfWeek dayOfWeek)
        {
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Allergens)
                            .ThenInclude(a => a.Allergens)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Images)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Ingredients)
                            .ThenInclude(i => i.Products)
                                .ThenInclude(mp => mp.Product)
                .FirstOrDefault(m => m.DayOfWeek == dayOfWeek);

            if (menu == null)
            {
                return NotFound();
            }

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpGet("week")]
        public ActionResult<List<MenuDto>> GetMenuForWeek()
        {
            var menus = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Allergens)
                            .ThenInclude(a => a.Allergens)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Images)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Ingredients)
                            .ThenInclude(i => i.Products)
                                .ThenInclude(mp => mp.Product)
                .OrderBy(m => m.DayOfWeek)
                .ToList();

            var menuDtos = menus.Select(menu => MapMenuToDto(menu)).ToList();

            return Ok(menuDtos);
        }

        [HttpPut("{id}/restaurant-open")]
        public ActionResult<MenuDto> UpdateRestaurantOpen(int id, [FromBody] UpdateRestaurantOpenDto updateRestaurantOpenDto)
        {
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Allergens)
                            .ThenInclude(a => a.Allergens)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Images)
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Ingredients)
                            .ThenInclude(i => i.Products)
                                .ThenInclude(mp => mp.Product)
                .FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            menu.IsRestaurantOpen = updateRestaurantOpenDto.IsOpen;

            _dbContext.SaveChanges();

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpPost("{id}/meals")]
        public ActionResult<MenuDto> AddMealToMenu(int id, AddMealToMenuDto addMealToMenuDto)
        {
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            var meal = _dbContext.Meals.Include(m => m.Pricing).FirstOrDefault(m => m.Id == addMealToMenuDto.MealId);

            if (meal == null)
            {
                return BadRequest($"Meal with ID {addMealToMenuDto.MealId} not found.");
            }

            menu.MenuMeals.Add(new MenuMeal { Meal = meal });

            _dbContext.SaveChanges();

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpPut("{id}/meals")]
        public ActionResult<MenuDto> UpdateMenuMeals(int id, [FromBody] UpdateMenuMealsDto updateMenuMealsDto)
        {
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            var mealsToAdd = _dbContext.Meals
                .Include(m => m.Pricing)
                .Where(m => updateMenuMealsDto.MealIdsToAdd.Contains(m.Id))
                .ToList();

            var menuMealsToRemove = menu.MenuMeals
                .Where(mm => updateMenuMealsDto.MealIdsToRemove.Contains(mm.Meal.Id))
                .ToList();

            foreach (var meal in mealsToAdd)
            {
                if (!menu.MenuMeals.Any(mm => mm.Meal.Id == meal.Id))
                {
                    menu.MenuMeals.Add(new MenuMeal { Meal = meal });
                }
            }

            foreach (var menuMeal in menuMealsToRemove)
            {
                menu.MenuMeals.Remove(menuMeal);
            }

            _dbContext.SaveChanges();

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpDelete("{id}/meals/{mealId}")]
        public ActionResult<MenuDto> RemoveMealFromMenu(int id, int mealId)
        {
            var menu = _dbContext.Menus
                .Include(m => m.MenuMeals)
                    .ThenInclude(mm => mm.Meal)
                        .ThenInclude(m => m.Pricing)
                .FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            var menuMeal = menu.MenuMeals.FirstOrDefault(mm => mm.Meal.Id == mealId);

            if (menuMeal == null)
            {
                return BadRequest($"Meal with ID {mealId} not found in the menu.");
            }

            menu.MenuMeals.Remove(menuMeal);

            _dbContext.SaveChanges();

            var menuDto = MapMenuToDto(menu);

            return Ok(menuDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            var menu = _dbContext.Menus.Find(id);

            if (menu == null)
            {
                return NotFound();
            }

            _dbContext.Menus.Remove(menu);
            _dbContext.SaveChanges();

            return NoContent();
        }

        private MenuDto MapMenuToDto(Menu menu)
        {
            return new MenuDto
            {
                Id = menu.Id,
                DayOfWeek = menu.DayOfWeek.ToString(),
                IsRestaurantOpen = menu.IsRestaurantOpen,
                Meals = menu.MenuMeals.Select(mm => new MenuMealDto
                {
                    Id = mm.Meal.Id,
                    Name = mm.Meal.Name,
                    Price = mm.Meal.Pricing != null ? mm.Meal.Pricing.Price : 0,
                    Products = mm.Meal.Ingredients?.Products.Select(mp => mp.Product.Name).ToList() ?? new List<string>(),
                    Allergens = mm.Meal.Allergens?.Allergens.Select(a => a.Name).ToList() ?? new List<string>(),
                    Description = mm.Meal.Description,
                    ImageUrls = mm.Meal.Images.Select(i => i.ImageUrl).ToList(),
                }).ToList()
            };
        }
    }

    public class AddMealToMenuDto
    {
        public int MealId { get; set; }
    }

    public class UpdateMenuMealsDto
    {
        public List<int> MealIdsToAdd { get; set; }
        public List<int> MealIdsToRemove { get; set; }
    }

    public class UpdateRestaurantOpenDto
    {
        public bool IsOpen { get; set; }
    }

    public class MenuDto
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsRestaurantOpen { get; set; }
        public List<MenuMealDto> Meals { get; set; }
    }

    public class MenuMealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<string> Products { get; set; }
        public List<string> Allergens { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrls { get; set; }
    }

    public class CreateMenuDto
    {
        public string DayOfWeek { get; set; }
        public bool IsRestaurantOpen { get; set; }
        public List<int> MealIds { get; set; }
    }

    public class UpdateMenuDto
    {
        public string DayOfWeek { get; set; }
        public bool IsRestaurantOpen { get; set; }
        public List<int> MealIds { get; set; }
    }*/
    }
}
