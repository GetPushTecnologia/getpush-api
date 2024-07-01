using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;
using Dapper;

namespace GetPush_Api.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<UsuarioLoginResult> GetUsuarioLogin(string email)
        {
            var query = @"select * 
                            from getpushAdmin.usuario 
                           where email = @email";

            using(var conn = new SqlConnection(Runtime.ConnectionString))
            {
                conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<UsuarioLoginResult>(query, new { email });
            }
        }
    }
}
