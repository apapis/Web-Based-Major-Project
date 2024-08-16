using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Services
{
    public class UnitService
    {
        private readonly IDbConnection _dbConnection;

        public UnitService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Unit>> GetUnitsAsync()
        {
            var sql = "SELECT * FROM Unit";
            var units = await _dbConnection.QueryAsync<Unit>(sql);
            return units.ToList();
        }

        public async Task<Unit> GetUnitByIdAsync(int id)
        {
            var sql = "SELECT * FROM Unit WHERE Id = @Id";
            var unit = await _dbConnection.QuerySingleOrDefaultAsync<Unit>(sql, new { Id = id });
            return unit;
        }

        public async Task AddUnitAsync(Unit unit)
        {
            ValidateUnit(unit);
            var sql = "INSERT INTO Unit (Name, Abbreviation) VALUES (@Name, @Abbreviation)";
            await _dbConnection.ExecuteAsync(sql, unit);
        }

        public async Task UpdateUnitAsync(Unit existingUnit)
        {
            ValidateUnit(existingUnit);
            var sql = "UPDATE Unit SET Name = @Name, Abbreviation = @Abbreviation WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, existingUnit);
        }

        public async Task DeleteUnitAsync(int id)
        {
            var sql = "DELETE FROM Unit WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        private void ValidateUnit(Unit unit)
        {
            var validationContext = new ValidationContext(unit, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(unit, validationContext, validationResults, validateAllProperties: true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }
        }
    }
}