using System.Collections.Generic;
using System.Linq;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Services
{
    public class AllergenService
    {
        private readonly RestaurantContext _dbContext;

        public AllergenService(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Allergen> GetAllergens()
        {
            return _dbContext.Allergens.ToList();
        }

        public Allergen GetAllergenById(int id)
        {
            return _dbContext.Allergens.FirstOrDefault(a => a.Id == id);
        }

        public void AddAllergen(Allergen allergen)
        {
            _dbContext.Allergens.Add(allergen);
            _dbContext.SaveChanges();
        }

        public void UpdateAllergen(Allergen existingAllergen, Allergen updatedAllergen)
        {
            existingAllergen.Name = updatedAllergen.Name;
            _dbContext.SaveChanges();
        }

        public void DeleteAllergen(Allergen allergen)
        {
            _dbContext.Allergens.Remove(allergen);
            _dbContext.SaveChanges();
        }
    }
}
