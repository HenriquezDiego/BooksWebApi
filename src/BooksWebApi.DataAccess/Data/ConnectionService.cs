using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;

namespace BooksWebApi.DataAccess.Data
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration _configuration;
        public readonly SqlConnection SqlConnection = new SqlConnection();

        public ConnectionService (IConfiguration configuration)
        {
            _configuration = configuration;
            SqlConnection.ConnectionString=configuration.GetConnectionString("DefaultConnection");
        }

        //Metodo para abrir la conexion
        public SqlConnection GetConnection()
        {
            return SqlConnection;
        }

        public void Open()
        {
            try
            {
                SqlConnection.Open();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Metodo para cerrar la conexion
        public void Close()
        {
            SqlConnection.Close();
        }
    }
}
