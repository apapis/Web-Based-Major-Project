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
    }
}
