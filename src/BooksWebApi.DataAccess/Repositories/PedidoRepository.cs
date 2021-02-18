using BooksWebApi.DataAccess.Data;
using BooksWebApi.Models.Entities;
using BooksWebApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BooksWebApi.DataAccess.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IConnectionService _connectionService;

        public PedidoRepository(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public Pedido Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> GetAll()
        {
            throw new NotImplementedException();
        }

        public (bool,int) Insert(Pedido entity)
        {
            var command = new SqlCommand
            {
                CommandText = "INSERT INTO Pedidos VALUES(@p1,@p2,@p3,@p4)"
            };

            var sqlCommand = new SqlCommand("select top(1) PedidoId from Pedidos order by PedidoId DESC", _connectionService.GetConnection())
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


            command.Parameters.AddWithValue("@p1",lastId);
            command.Parameters.AddWithValue("@p2", entity.ClienteId);
            command.Parameters.AddWithValue("@p3", entity.LibroId);
            command.Parameters.AddWithValue("@p4", entity.Fecha);
            command.Connection = _connectionService.GetConnection();


            var executeNonQuery = command.ExecuteNonQuery();
            _connectionService.Close();

            return (executeNonQuery <= 0,lastId);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Pedido entity)
        {
            throw new NotImplementedException();
        }

        public PedidoDto GetDto(int id)
        {
            var sqlCommand = new SqlCommand($"select p.PedidoId,c.ClienteId,c.Nombres,c.Apellidos,l.Titulo,p.Fecha from Pedidos p inner join Clientes c on p.ClienteId = c.ClienteId inner join Libros l on p.LibroId = l.LibroId where p.PedidoId = {id}", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var pedido = new PedidoDto();

            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    pedido = new PedidoDto()
                    {
                        PedidoId = int.Parse(reader[0]?.ToString() ?? "0"),
                        ClienteId = int.Parse(reader[1].ToString() ?? "0"),
                        Nombres = reader[2].ToString(),
                        Apellidos = reader[3].ToString(),
                        LibroId = int.Parse(reader[4].ToString() ?? "0"),
                        Libro = reader[5].ToString(),
                        Fecha = reader.GetDateTime(6)
                    };
                }
            }

            _connectionService.Close();
            return pedido;
        }

        public IEnumerable<PedidoDto> GetAllDtos()
        {
            var sqlCommand = new SqlCommand("select p.PedidoId,c.ClienteId,c.Nombres,c.Apellidos,l.Titulo,p.Fecha from Pedidos p inner join Clientes c on p.ClienteId = c.ClienteId inner join Libros l on p.LibroId = l.LibroId", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var pedidos = new List<PedidoDto>();

            using(var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    pedidos.Add(new PedidoDto
                    {
                        PedidoId = int.Parse(reader[0]?.ToString() ?? "0"),
                        ClienteId = int.Parse(reader[1].ToString() ?? "0"),
                        Nombres = reader[2].ToString(),
                        Apellidos = reader[3].ToString(),
                        Libro = reader[4].ToString(),
                        Fecha = DateTime.Parse(reader[5].ToString() ?? "2021-2-18")
                    });
                }
            }

            _connectionService.Close();
            return pedidos;
        }
    }
}
