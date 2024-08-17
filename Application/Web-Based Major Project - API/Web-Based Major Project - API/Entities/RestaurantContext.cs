using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web_Based_Major_Project___API.Entities
{
    public class RestaurantContext : IdentityDbContext<ApplicationUser>
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealPricing> MealPricings { get; set; }
        public DbSet<MealIngredients> MealIngredients { get; set; }
        public DbSet<MealAllergens> MealAllergens { get; set; }
        public DbSet<MealProduct> MealProducts { get; set; }
        public DbSet<MealCost> MealCosts { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<MealImage> MealImages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<MenuMeal> MenuMeals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Pricing)
                .WithOne()
                .HasForeignKey<MealPricing>(mp => mp.Id);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Ingredients)
                .WithOne()
                .HasForeignKey<MealIngredients>(mi => mi.Id);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Allergens)
                .WithOne(ma => ma.Meal)
                .HasForeignKey<MealAllergens>(ma => ma.MealId);

            modelBuilder.Entity<MealPricing>()
                .HasMany(mp => mp.Costs)
                .WithOne(mc => mc.MealPricing)
                .HasForeignKey(mc => mc.MealPricingId);

            modelBuilder.Entity<MealIngredients>()
                .HasMany(mi => mi.Products)
                .WithOne(mp => mp.MealIngredients)
                .HasForeignKey(mp => mp.MealIngredientsId);

            modelBuilder.Entity<MealProduct>()
                .HasKey(mp => new { mp.MealIngredientsId, mp.ProductId });

            modelBuilder.Entity<MealProduct>()
                .HasOne(mp => mp.MealIngredients)
                .WithMany(mi => mi.Products)
                .HasForeignKey(mp => mp.MealIngredientsId);

            modelBuilder.Entity<MealProduct>()
                .HasOne(mp => mp.Product)
                .WithMany()
                .HasForeignKey(mp => mp.ProductId);

            modelBuilder.Entity<MealAllergens>()
                .HasMany(ma => ma.Allergens)
                .WithMany()
                .UsingEntity(j => j.ToTable("MealAllergenMap"));

            /*modelBuilder.Entity<Product>()
                .HasMany(p => p.Allergens)
                .WithMany()
                .UsingEntity(j => j.ToTable("ProductAllergens"));*/

            modelBuilder.Entity<Menu>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<MenuMeal>()
                .HasKey(mm => new { mm.MenuId, mm.MealId });

            modelBuilder.Entity<MenuMeal>()
                .HasOne(mm => mm.Menu)
                .WithMany(m => m.MenuMeals)
                .HasForeignKey(mm => mm.MenuId);

            modelBuilder.Entity<MenuMeal>()
                .HasOne(mm => mm.Meal)
                .WithMany(m => m.MenuMeals)
                .HasForeignKey(mm => mm.MealId);

            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, DayOfWeek = DayOfWeek.Monday, IsRestaurantOpen = true },
                new Menu { Id = 2, DayOfWeek = DayOfWeek.Tuesday, IsRestaurantOpen = true },
                new Menu { Id = 3, DayOfWeek = DayOfWeek.Wednesday, IsRestaurantOpen = true },
                new Menu { Id = 4, DayOfWeek = DayOfWeek.Thursday, IsRestaurantOpen = true },
                new Menu { Id = 5, DayOfWeek = DayOfWeek.Friday, IsRestaurantOpen = true },
                new Menu { Id = 6, DayOfWeek = DayOfWeek.Saturday, IsRestaurantOpen = true },
                new Menu { Id = 7, DayOfWeek = DayOfWeek.Sunday, IsRestaurantOpen = true }
            );
        }

    }
}