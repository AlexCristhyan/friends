using Dapper;
using Friends.Domain.Friend.Models;
using Friends.Domain.Friend.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Friends.Infrastructure.Sql.DAO
{
    public class FriendDAO  : IFriendRepository
    {
        private IConfiguration _configuration;

        public FriendDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Create(FriendModel friend)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("Default")))
            {
                String query = "INSERT INTO dbo.Friend (name,latitude,longitude) VALUES (@name,@latitude, @longitude)";

                using (SqlCommand command = new SqlCommand(query, conexao))
                {
                    command.Parameters.AddWithValue("@name", friend.Name);
                    command.Parameters.AddWithValue("@latitude", friend.Latitude);
                    command.Parameters.AddWithValue("@longitude", friend.Longitude);

                    conexao.Open();
                    int result = command.ExecuteNonQuery();
                    conexao.Close();

                    if (result < 0)
                        return false;

                    return true;
                }
              
            }
        }

        public List<FriendModel> Get()
        {
            List<FriendModel> list = new List<FriendModel>();
            using (SqlConnection conexao = new SqlConnection(
                 _configuration.GetConnectionString("Default")))
            {
                var query = conexao.Query<FriendModel>(
                    "SELECT Id,Name, Latitude, Longitude " +
                    "FROM dbo.Friend");
                
                foreach (var item in query)
                {
                    FriendModel model = new FriendModel();
                    model.Id = item.Id;
                    model.Name = item.Name;
                    model.Latitude = item.Latitude;
                    model.Longitude = item.Longitude;

                    list.Add(model);
                }
            }
            return list;
        }
    }
}
