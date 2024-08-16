using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Web_Based_Major_Project___API.Data
{
    public class DapperRoleStore : IRoleStore<IdentityRole>
    {
        private readonly IDbConnection _connection;

        public DapperRoleStore(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = @"
                INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
                VALUES (@Id, @Name, @NormalizedName, @ConcurrencyStamp)";

            try
            {
                await _connection.ExecuteAsync(sql, role);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = "DELETE FROM AspNetRoles WHERE Id = @Id";

            try
            {
                await _connection.ExecuteAsync(sql, new { role.Id });
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = "SELECT * FROM AspNetRoles WHERE Id = @Id";

            return await _connection.QuerySingleOrDefaultAsync<IdentityRole>(sql, new { Id = roleId });
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = "SELECT * FROM AspNetRoles WHERE NormalizedName = @NormalizedName";

            return await _connection.QuerySingleOrDefaultAsync<IdentityRole>(sql, new { NormalizedName = normalizedRoleName });
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = @"
                UPDATE AspNetRoles 
                SET Name = @Name, 
                    NormalizedName = @NormalizedName, 
                    ConcurrencyStamp = @ConcurrencyStamp
                WHERE Id = @Id";

            try
            {
                await _connection.ExecuteAsync(sql, role);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
