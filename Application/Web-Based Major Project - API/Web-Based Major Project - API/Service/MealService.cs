using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Web_Based_Major_Project___API.DTO;
using Web_Based_Major_Project___API.Entities.Meal;

namespace Web_Based_Major_Project___API.Services
{
    public class MealService
    {
        private readonly IDbConnection _dbConnection;

        public MealService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            var meals = await _dbConnection.QueryAsync<Meal>("SELECT * FROM Meal");
            foreach (var meal in meals)
            {
                await PopulateMealRelatedData(meal);
            }
            return meals;
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            var meal = await _dbConnection.QuerySingleOrDefaultAsync<Meal>("SELECT * FROM Meal WHERE Id = @Id", new { Id = id });
            if (meal != null)
            {
                await PopulateMealRelatedData(meal);
            }
            return meal;
        }

        public async Task<int> CreateMealAsync(MealDTO mealDto)
        {
            ValidateMealDto(mealDto);
            var meal = MapDtoToMeal(mealDto);

            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open(); // U¿ycie metody synchronicznej
            }

            using var transaction = _dbConnection.BeginTransaction();
            try
            {
                var sql = @"
            INSERT INTO Meal (Name, Description, Price)
            VALUES (@Name, @Description, @Price);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = await _dbConnection.QuerySingleAsync<int>(sql, meal, transaction);
                meal.Id = id;

                await InsertMealRelatedData(meal, transaction);

                transaction.Commit();
                return id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }



        public async Task<bool> UpdateMealAsync(int id, MealDTO mealDto)
        {
            ValidateMealDto(mealDto);
            var meal = MapDtoToMeal(mealDto);
            meal.Id = id;

            // Sprawdzenie, czy po³¹czenie jest otwarte
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open(); // Otwórz po³¹czenie, jeœli jest zamkniête
            }

            using var transaction = _dbConnection.BeginTransaction();
            try
            {
                var sql = @"
            UPDATE Meal 
            SET Name = @Name, Description = @Description, Price = @Price
            WHERE Id = @Id";
                var affectedRows = await _dbConnection.ExecuteAsync(sql, meal, transaction);

                if (affectedRows == 0)
                {
                    return false;
                }

                await DeleteMealRelatedData(meal.Id, transaction);
                await InsertMealRelatedData(meal, transaction);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close(); // Zamknij po³¹czenie po zakoñczeniu operacji
                }
            }
        }


        public async Task<bool> DeleteMealAsync(int id)
        {
            // Sprawdzenie, czy po³¹czenie jest otwarte
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open(); // Otwórz po³¹czenie, jeœli jest zamkniête
            }
            using var transaction = _dbConnection.BeginTransaction();
            try
            {
                await DeleteMealRelatedData(id, transaction);

                var sql = "DELETE FROM Meal WHERE Id = @Id";
                var affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

                transaction.Commit();
                return affectedRows > 0;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close(); // Zamknij po³¹czenie po zakoñczeniu operacji
                }
            }
        }

        private async Task PopulateMealRelatedData(Meal meal)
        {
            meal.Images = (await _dbConnection.QueryAsync<MealImage>("SELECT * FROM MealImage WHERE MealId = @Id", new { meal.Id })).ToList();
            meal.MealProducts = (await _dbConnection.QueryAsync<MealProduct>("SELECT * FROM MealProduct WHERE MealId = @Id", new { meal.Id })).ToList();
            meal.MealAllergenIds = (await _dbConnection.QueryAsync<int>("SELECT AllergenId FROM MealAllergen WHERE MealId = @Id", new { meal.Id })).ToList();
            meal.MealPricing = await _dbConnection.QuerySingleOrDefaultAsync<MealPricing>("SELECT * FROM MealPricing WHERE MealId = @Id", new { meal.Id });
            meal.MealCosts = (await _dbConnection.QueryAsync<MealCost>("SELECT * FROM MealCost WHERE MealId = @Id", new { meal.Id })).ToList();
        }

        private async Task InsertMealRelatedData(Meal meal, IDbTransaction transaction)
        {
            foreach (var image in meal.Images)
            {
                var imageSql = "INSERT INTO MealImage (ImageUrl, MealId) VALUES (@ImageUrl, @MealId); SELECT CAST(SCOPE_IDENTITY() as int)";
                image.Id = await _dbConnection.QuerySingleAsync<int>(imageSql, new { image.ImageUrl, MealId = meal.Id }, transaction);
            }

            foreach (var product in meal.MealProducts)
            {
                var productSql = "INSERT INTO MealProduct (ProductId, MealId, Quantity) VALUES (@ProductId, @MealId, @Quantity); SELECT CAST(SCOPE_IDENTITY() as int)";
                product.Id = await _dbConnection.QuerySingleAsync<int>(productSql, new { product.ProductId, MealId = meal.Id, product.Quantity }, transaction);
            }

            foreach (var allergenId in meal.MealAllergenIds)
            {
                await _dbConnection.ExecuteAsync(
                    "INSERT INTO MealAllergen (MealId, AllergenId) VALUES (@MealId, @AllergenId)",
                    new { MealId = meal.Id, AllergenId = allergenId },
                    transaction
                );
            }

            if (meal.MealPricing != null)
            {
                // Wstawienie rekordu do MealPricing z prawid³owym MealId
                var pricingSql = @"
            INSERT INTO MealPricing (MealId, NumberOfPeople, CostOfAllIngredients, CostOfMakeIt, ProposedPrice) 
            VALUES (@MealId, @NumberOfPeople, @CostOfAllIngredients, @CostOfMakeIt, @ProposedPrice);
            SELECT CAST(SCOPE_IDENTITY() as int)";
                meal.MealPricing.Id = await _dbConnection.QuerySingleAsync<int>(pricingSql,
                    new
                    {
                        MealId = meal.Id, // Przypisujemy MealId
                        meal.MealPricing.NumberOfPeople,
                        meal.MealPricing.CostOfAllIngredients,
                        meal.MealPricing.CostOfMakeIt,
                        meal.MealPricing.ProposedPrice
                    },
                    transaction);

                // Aktualizuj Meal z MealPricingId
                var updateMealSql = "UPDATE Meal SET MealPricingId = @MealPricingId WHERE Id = @MealId";
                await _dbConnection.ExecuteAsync(updateMealSql, new { MealPricingId = meal.MealPricing.Id, MealId = meal.Id }, transaction);
            }

            foreach (var cost in meal.MealCosts)
            {
                var costSql = "INSERT INTO MealCost (Name, Value, MealId) VALUES (@Name, @Value, @MealId); SELECT CAST(SCOPE_IDENTITY() as int)";
                cost.Id = await _dbConnection.QuerySingleAsync<int>(costSql, new { cost.Name, cost.Value, MealId = meal.Id }, transaction);
            }
        }



        private async Task DeleteMealRelatedData(int mealId, IDbTransaction transaction)
        {
            await _dbConnection.ExecuteAsync("DELETE FROM MealImage WHERE MealId = @Id", new { Id = mealId }, transaction);
            await _dbConnection.ExecuteAsync("DELETE FROM MealProduct WHERE MealId = @Id", new { Id = mealId }, transaction);
            await _dbConnection.ExecuteAsync("DELETE FROM MealAllergen WHERE MealId = @Id", new { Id = mealId }, transaction);
            await _dbConnection.ExecuteAsync("DELETE FROM MealPricing WHERE MealId = @Id", new { Id = mealId }, transaction);
            await _dbConnection.ExecuteAsync("DELETE FROM MealCost WHERE MealId = @Id", new { Id = mealId }, transaction);
        }

        private void ValidateMealDto(MealDTO mealDto)
        {
            var validationContext = new ValidationContext(mealDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(mealDto, validationContext, validationResults, validateAllProperties: true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }

            // Validate MealProducts
            foreach (var product in mealDto.MealProducts)
            {
                var productContext = new ValidationContext(product, serviceProvider: null, items: null);
                if (!Validator.TryValidateObject(product, productContext, validationResults, validateAllProperties: true))
                {
                    throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
                }
            }

            // Validate MealPricing
            if (mealDto.MealPricing != null)
            {
                var pricingContext = new ValidationContext(mealDto.MealPricing, serviceProvider: null, items: null);
                if (!Validator.TryValidateObject(mealDto.MealPricing, pricingContext, validationResults, validateAllProperties: true))
                {
                    throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
                }
            }

            // Validate MealCosts
            foreach (var cost in mealDto.MealCosts)
            {
                var costContext = new ValidationContext(cost, serviceProvider: null, items: null);
                if (!Validator.TryValidateObject(cost, costContext, validationResults, validateAllProperties: true))
                {
                    throw new ValidationException(string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
                }
            }
        }

        private Meal MapDtoToMeal(MealDTO dto)
        {
            return new Meal
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Images = dto.Images.Select(i => new MealImage { ImageUrl = i.ImageUrl }).ToList(),
                MealProducts = dto.MealProducts.Select(p => new MealProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList(),
                MealPricing = dto.MealPricing != null ? new MealPricing
                {
                    NumberOfPeople = dto.MealPricing.NumberOfPeople,
                    CostOfAllIngredients = dto.MealPricing.CostOfAllIngredients,
                    CostOfMakeIt = dto.MealPricing.CostOfMakeIt,
                    ProposedPrice = dto.MealPricing.ProposedPrice
                } : null,
                MealAllergenIds = dto.MealAllergenIds,
                MealCosts = dto.MealCosts.Select(c => new MealCost { Name = c.Name, Value = c.Value }).ToList()
            };
        }
    }
}