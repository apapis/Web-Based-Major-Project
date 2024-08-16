using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Services
{
    public class AllergenService
    {
        private readonly IDbConnection _dbConnection;

        public AllergenService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Allergen>> GetAllergensAsync()
        {
            var sql = "SELECT * FROM Allergens";
            var allergens = await _dbConnection.QueryAsync<Allergen>(sql);
            return allergens.ToList();
        }

        public async Task<Allergen> GetAllergenByIdAsync(int id)
        {
            var sql = "SELECT * FROM Allergens WHERE Id = @Id";
            var allergen = await _dbConnection.QuerySingleOrDefaultAsync<Allergen>(sql, new { Id = id });
            return allergen;
        }

        public async Task AddAllergenAsync(Allergen allergen)
        {
            ValidateAllergen(allergen);

            var sql = "INSERT INTO Allergens (Name) VALUES (@Name)";
            await _dbConnection.ExecuteAsync(sql, allergen);
        }

        public async Task UpdateAllergenAsync(Allergen existingAllergen)
        {
            ValidateAllergen(existingAllergen);

            var sql = "UPDATE Allergens SET Name = @Name WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { existingAllergen.Name, existingAllergen.Id });
        }

        public async Task DeleteAllergenAsync(int id)
        {
            var sql = "DELETE FROM Allergens WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        private void ValidateAllergen(Allergen allergen)
        {
            var validationContext = new ValidationContext(allergen, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(allergen, validationContext, validationResults, validateAllProperties: true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }
        }
    }
}
