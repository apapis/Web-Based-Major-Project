using Dapper;
using System.Data;

namespace Web_Based_Major_Project___API.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(IDbConnection dbConnection)
        {
            CreateAllergensTable(dbConnection);
            CreateStoreTable(dbConnection);
            CreateUnitTable(dbConnection);
            CreateProductsTable(dbConnection);
            CreateProductAllergensTable(dbConnection);
            CreateMealTable(dbConnection);
            CreateMealProductTable(dbConnection);
            CreateMealImageTable(dbConnection);
            CreateMealPricingTable(dbConnection);
            CreateMealCostTable(dbConnection);
            CreateMealAllergenTable(dbConnection);
        }

        private static void CreateAllergensTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Allergens')
            BEGIN
                CREATE TABLE Allergens (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) NOT NULL
                );
            END;";

            dbConnection.Execute(createTableQuery);
        }

        private static void CreateStoreTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Store')
            BEGIN
                CREATE TABLE Store (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(50) NOT NULL
                );
            END;";

            dbConnection.Execute(createTableQuery);
        }

        private static void CreateUnitTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Unit')
            BEGIN
                CREATE TABLE Unit (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(50) NOT NULL,
                    Abbreviation NVARCHAR(50)
                );
            END;";

            dbConnection.Execute(createTableQuery);
        }

        private static void CreateProductsTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
            BEGIN
                CREATE TABLE Products (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) NOT NULL,
                    StoreId INT NOT NULL,
                    Quantity INT NOT NULL,
                    UnitId INT NOT NULL,
                    Price FLOAT NOT NULL,
                    PricePerUnit FLOAT NOT NULL,
                    FOREIGN KEY (StoreId) REFERENCES Store(Id),
                    FOREIGN KEY (UnitId) REFERENCES Unit(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateProductAllergensTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductAllergens')
            BEGIN
                CREATE TABLE ProductAllergens (
                    ProductId INT,
                    AllergenId INT,
                    PRIMARY KEY (ProductId, AllergenId),
                    FOREIGN KEY (ProductId) REFERENCES Products(Id),
                    FOREIGN KEY (AllergenId) REFERENCES Allergens(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateMealTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Meal')
            BEGIN
                CREATE TABLE Meal (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) NOT NULL,
                    Description NVARCHAR(MAX),
                    Price FLOAT NOT NULL,
                    MealPricingId INT
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateMealProductTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MealProduct')
            BEGIN
                CREATE TABLE MealProduct (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    ProductId INT NOT NULL,
                    MealId INT NOT NULL,
                    Quantity FLOAT NOT NULL,
                    FOREIGN KEY (ProductId) REFERENCES Products(Id),
                    FOREIGN KEY (MealId) REFERENCES Meal(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateMealImageTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MealImage')
            BEGIN
                CREATE TABLE MealImage (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    ImageUrl NVARCHAR(255) NOT NULL,
                    MealId INT NOT NULL,
                    FOREIGN KEY (MealId) REFERENCES Meal(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateMealPricingTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MealPricing')
            BEGIN
                CREATE TABLE MealPricing (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    MealId INT NOT NULL,
                    NumberOfPeople INT NOT NULL,
                    CostOfAllIngredients FLOAT NOT NULL,
                    CostOfMakeIt FLOAT NOT NULL,
                    ProposedPrice FLOAT NOT NULL,
                    FOREIGN KEY (MealId) REFERENCES Meal(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateMealCostTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MealCost')
            BEGIN
                CREATE TABLE MealCost (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) NOT NULL,
                    Value FLOAT NOT NULL,
                    MealId INT NOT NULL,
                    FOREIGN KEY (MealId) REFERENCES Meal(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }

        private static void CreateMealAllergenTable(IDbConnection dbConnection)
        {
            var createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MealAllergen')
            BEGIN
                CREATE TABLE MealAllergen (
                    MealId INT,
                    AllergenId INT,
                    PRIMARY KEY (MealId, AllergenId),
                    FOREIGN KEY (MealId) REFERENCES Meal(Id),
                    FOREIGN KEY (AllergenId) REFERENCES Allergens(Id)
                );
            END;";
            dbConnection.Execute(createTableQuery);
        }
    }
}
