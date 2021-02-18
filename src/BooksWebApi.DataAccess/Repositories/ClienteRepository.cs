using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BooksWebApi.DataAccess.Data;
using BooksWebApi.Models.Entities;

namespace BooksWebApi.DataAccess.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConnectionService _connectionService;

        public ClienteRepository(IConnectionService connection)
        {
            _connectionService = connection;
        }
        public Cliente Get(int id)
        {
            var sqlCommand = new SqlCommand($"Select * from Clientes where ClienteId={id}", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var cliente = new Cliente();

            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        ClienteId = int.Parse(reader[0]?.ToString() ?? "0"),
                        Nombres = reader[1].ToString(),
                        Apellidos =  reader[2].ToString(),
                        FechaNacimiento = DateTime.Parse(reader[3].ToString() ?? "2021-2-18"),
                        Telefono = reader[4].ToString(),
                        Email = reader[5].ToString()
                    };
                }
            }

            _connectionService.Close();
            return cliente;
        }

        public IEnumerable<Cliente> GetAll()
        {
            var sqlCommand = new SqlCommand("Select * from Clientes",
                _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var clientes = new List<Cliente>();

            using(var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        ClienteId = int.Parse(reader[0]?.ToString() ?? "0"),
                        Nombres = reader[1].ToString(),
                        Apellidos =  reader[2].ToString(),
                        FechaNacimiento = DateTime.Parse(reader[3].ToString() ?? "2021-2-18"),
                        Telefono = reader[4].ToString(),
                        Email = reader[5].ToString()
                    });
                }
            }

            _connectionService.Close();
            return clientes;
        }

        public bool Insert(Cliente entity)
        {
            var command = new SqlCommand
            {
                CommandText = "INSERT INTO Clientes VALUES(@clienteId,@nombres,@apellidos,@fecha,@telefono,@email)"
            };

            var sqlCommand = new SqlCommand("select top(1) clienteId from Clientes order by clienteId DESC", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();
            var lastId = 0;
            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    lastId = int.Parse(reader[0].ToString()??"0");
                    lastId++;
                }
            }


            command.Parameters.AddWithValue("@clienteId",lastId);
            command.Parameters.AddWithValue("@nombres", entity.Nombres);
            command.Parameters.AddWithValue("@apellidos", entity.Apellidos);
            command.Parameters.AddWithValue("@fecha", entity.FechaNacimiento);
            command.Parameters.AddWithValue("@telefono", entity.Telefono);
            command.Parameters.AddWithValue("@email", entity.Email);
            command.Connection = _connectionService.GetConnection();


            var executeNonQuery = command.ExecuteNonQuery();
            _connectionService.Close();

            return executeNonQuery <= 0;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
