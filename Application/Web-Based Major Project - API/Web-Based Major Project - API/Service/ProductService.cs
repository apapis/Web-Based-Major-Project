using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web_Based_Major_Project___API.Entities;
using Web_Based_Major_Project___API.Models;

namespace Web_Based_Major_Project___API.Services
{
    public class ProductService
    {
        private readonly IDbConnection _dbConnection;

        public ProductService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ProductResponseDTO>> GetProductsAsync()
        {
            var sql = @"
                SELECT p.*, s.*, u.*, a.*
                FROM Products p
                LEFT JOIN Store s ON p.StoreId = s.Id
                LEFT JOIN Unit u ON p.UnitId = u.Id
                LEFT JOIN ProductAllergens pa ON p.Id = pa.ProductId
                LEFT JOIN Allergens a ON pa.AllergenId = a.Id";

            var productDictionary = new Dictionary<int, ProductResponseDTO>();

            await _dbConnection.QueryAsync<Product, Store, Unit, Allergen, ProductResponseDTO>(
                sql,
                (product, store, unit, allergen) =>
                {
                    if (!productDictionary.TryGetValue(product.Id, out var productDto))
                    {
                        productDto = MapToProductResponseDTO(product, store, unit);
                        productDictionary.Add(product.Id, productDto);
                    }

                    if (allergen != null && !productDto.Allergens.Any(a => a.Id == allergen.Id))
                    {
                        productDto.Allergens.Add(allergen);
                    }

                    return productDto;
                },
                splitOn: "Id,Id,Id"
            );

            return productDictionary.Values.ToList();
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            var sql = @"
                SELECT p.*, s.*, u.*, a.*
                FROM Products p
                LEFT JOIN Store s ON p.StoreId = s.Id
                LEFT JOIN Unit u ON p.UnitId = u.Id
                LEFT JOIN ProductAllergens pa ON p.Id = pa.ProductId
                LEFT JOIN Allergens a ON pa.AllergenId = a.Id
                WHERE p.Id = @Id";

            var productDto = new ProductResponseDTO();

            await _dbConnection.QueryAsync<Product, Store, Unit, Allergen, ProductResponseDTO>(
                sql,
                (product, store, unit, allergen) =>
                {
                    if (productDto.Id == 0)
                    {
                        productDto = MapToProductResponseDTO(product, store, unit);
                    }

                    if (allergen != null && !productDto.Allergens.Any(a => a.Id == allergen.Id))
                    {
                        productDto.Allergens.Add(allergen);
                    }

                    return productDto;
                },
                new { Id = id },
                splitOn: "Id,Id,Id"
            );

            return productDto.Id != 0 ? productDto : null;
        }

        public async Task<ProductResponseDTO> CreateProductAsync(Product product)
        {
            ValidateProduct(product);
            await ValidateAllergenIdsExistAsync(product.AllergenIds);
            await ValidateUnitExistsAsync(product.UnitId);
            await ValidateStoreExistsAsync(product.StoreId);

            var sql = @"
            INSERT INTO Products (Name, StoreId, Quantity, UnitId, Price, PricePerUnit)
            VALUES (@Name, @StoreId, @Quantity, @UnitId, @Price, @PricePerUnit);
            SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _dbConnection.QuerySingleAsync<int>(sql, product);
            product.Id = id;

            await UpdateProductAllergensAsync(id, product.AllergenIds);
            var allergens = await GetAllergensForProduct(id);

            return await GetProductByIdAsync(id);
        }

        public async Task<ProductResponseDTO> UpdateProductAsync(Product product)
        {
            ValidateProduct(product);
            await ValidateAllergenIdsExistAsync(product.AllergenIds);
            await ValidateUnitExistsAsync(product.UnitId);
            await ValidateStoreExistsAsync(product.StoreId);

            var sql = @"
            UPDATE Products 
            SET Name = @Name, StoreId = @StoreId, Quantity = @Quantity, 
                UnitId = @UnitId, Price = @Price, PricePerUnit = @PricePerUnit
            WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, product);

            await UpdateProductAllergensAsync(product.Id, product.AllergenIds);

            return await GetProductByIdAsync(product.Id);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _dbConnection.ExecuteAsync("DELETE FROM ProductAllergens WHERE ProductId = @Id", new { Id = id });
            await _dbConnection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = id });
        }

        private async Task<List<Allergen>> GetAllergensForProduct(int productId)
        {
            var sql = @"
                SELECT a.Id, a.Name
                FROM Allergens a
                INNER JOIN ProductAllergens pa ON a.Id = pa.AllergenId
                WHERE pa.ProductId = @ProductId";
            return (await _dbConnection.QueryAsync<Allergen>(sql, new { ProductId = productId })).ToList();
        }

        private async Task UpdateProductAllergensAsync(int productId, List<int> allergenIds)
        {
            await _dbConnection.ExecuteAsync("DELETE FROM ProductAllergens WHERE ProductId = @ProductId", new { ProductId = productId });
            foreach (var allergenId in allergenIds)
            {
                await _dbConnection.ExecuteAsync(
                    "INSERT INTO ProductAllergens (ProductId, AllergenId) VALUES (@ProductId, @AllergenId)",
                    new { ProductId = productId, AllergenId = allergenId });
            }
        }

        private ProductResponseDTO MapToProductResponseDTO(Product product, Store store, Unit unit)
        {
            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Store = store,
                Quantity = product.Quantity,
                Unit = unit,
                Price = product.Price,
                PricePerUnit = product.PricePerUnit,
                Allergens = new List<Allergen>()
            };
        }

        private void ValidateProduct(Product product)
        {
            var validationContext = new ValidationContext(product, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(product, validationContext, validationResults, validateAllProperties: true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }

            if (product.AllergenIds.Any(id => id <= 0))
            {
                throw new ValidationException("All allergen IDs must be positive numbers.");
            }
        }

        private async Task ValidateAllergenIdsExistAsync(List<int> allergenIds)
        {
            if (allergenIds == null || !allergenIds.Any())
            {
                return;
            }

            var sql = "SELECT Id FROM Allergens WHERE Id IN @Ids";
            var existingIds = await _dbConnection.QueryAsync<int>(sql, new { Ids = allergenIds });

            var nonExistingIds = allergenIds.Except(existingIds).ToList();
            if (nonExistingIds.Any())
            {
                throw new ValidationException($"The following allergen IDs do not exist: {string.Join(", ", nonExistingIds)}");
            }
        }

        private async Task ValidateStoreExistsAsync(int storeId)
        {
            var sql = "SELECT COUNT(1) FROM Store WHERE Id = @StoreId";
            var exists = await _dbConnection.ExecuteScalarAsync<bool>(sql, new { StoreId = storeId });
            if (!exists)
            {
                throw new ValidationException($"Store with ID {storeId} does not exist.");
            }
        }

        private async Task ValidateUnitExistsAsync(int unitId)
        {
            var sql = "SELECT COUNT(1) FROM Unit WHERE Id = @UnitId";
            var exists = await _dbConnection.ExecuteScalarAsync<bool>(sql, new { UnitId = unitId });
            if (!exists)
            {
                throw new ValidationException($"Unit with ID {unitId} does not exist.");
            }
        }
    }
}