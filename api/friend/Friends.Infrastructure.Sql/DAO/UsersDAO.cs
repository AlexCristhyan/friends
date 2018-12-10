using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Friends.Domain.User;
using Friends.Domain.User.Repositories;
using System.Threading.Tasks;

namespace Friends.Infrastructure.Sql.DAO
{
    public class UsersDAO : IUserRepository
    {
        private IConfiguration _configuration;

        public UsersDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserModel Find(string userID)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("Default")))
            {
                return  conexao.QueryFirstOrDefault<UserModel>(
                    "SELECT UserID, AccessKey " +
                    "FROM dbo.Users " +
                    "WHERE UserID = @UserID", new { UserID = userID });
            }
        }
    }
}
