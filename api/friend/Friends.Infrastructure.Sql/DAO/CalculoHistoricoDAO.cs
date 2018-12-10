using Friends.Domain.CalculoHistorico;
using Friends.Domain.CalculoHistorico.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Friends.Infrastructure.Sql.DAO
{
    public class CalculoHistoricoDAO : ICalculoHistoricoRepository
    {
        private IConfiguration _configuration;

        public CalculoHistoricoDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Create(CalculoHistoricoModel calculo)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("Default")))
            {
                String query = "INSERT INTO dbo.CalculoHistoricoLog (resultadoDistancia, diferencaLatitude, diferencaLongitude, dataCalculo, FriendId) " +
                                "VALUES (@resultadoDistancia,@diferencaLatitude, @diferencaLongitude, @dataCalculo, @FriendId)";

                using (SqlCommand command = new SqlCommand(query, conexao))
                {
                    command.Parameters.AddWithValue("@resultadoDistancia", calculo.ResultadoDistancia);
                    command.Parameters.AddWithValue("@diferencaLatitude", calculo.DiferencaLatitude);
                    command.Parameters.AddWithValue("@diferencaLongitude", calculo.DiferencaLongitude);
                    command.Parameters.AddWithValue("@dataCalculo", calculo.DataCalculo);
                    command.Parameters.AddWithValue("@FriendId", calculo.FriendId);

                    conexao.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                        return false;

                    return true;
                }

            }
        }
    }
}
