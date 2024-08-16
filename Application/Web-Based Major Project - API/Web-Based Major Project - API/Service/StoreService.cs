using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web_Based_Major_Project___API.Entities;

namespace Web_Based_Major_Project___API.Services
{
    public class StoreService
    {
        private readonly IDbConnection _dbConnection;

        public StoreService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Store>> GetStoresAsync()
        {
            var sql = "SELECT * FROM Store";
            var stores = await _dbConnection.QueryAsync<Store>(sql);
            return stores.ToList();
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            var sql = "SELECT * FROM Store WHERE Id = @Id";
            var store = await _dbConnection.QuerySingleOrDefaultAsync<Store>(sql, new { Id = id });
            return store;
        }

        public async Task AddStoreAsync(Store store)
        {
            ValidateStore(store);

            var sql = "INSERT INTO Store (Name) VALUES (@Name)";
            await _dbConnection.ExecuteAsync(sql, store);
        }

        public async Task UpdateStoreAsync(Store existingStore)
        {
            ValidateStore(existingStore);

            var sql = "UPDATE Store SET Name = @Name WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { existingStore.Name, existingStore.Id });
        }

        public async Task DeleteStoreAsync(int id)
        {
            var sql = "DELETE FROM Store WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        private void ValidateStore(Store store)
        {
            var validationContext = new ValidationContext(store, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(store, validationContext, validationResults, validateAllProperties: true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }
        }
    }
}
