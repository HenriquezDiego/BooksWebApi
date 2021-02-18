using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BooksWebApi.DataAccess.Data;
using BooksWebApi.Models.Entities;

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
            var sqlCommand = new SqlCommand($"select p.PedidoId,c.ClienteId,c.Nombres,c.Apellidos,l.Titulo,p.Fecha from Pedidos p inner join Clientes c on p.ClienteId = c.ClienteId inner join Libros l on p.LibroId = l.LibroIdwhere p.PedidoId = {id}", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var pedido = new Pedido();

            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    pedido = new Pedido
                    {
                        PedidoId = int.Parse(reader[0]?.ToString() ?? "0"),
                        //TODO
                    };
                }
            }

            _connectionService.Close();
            return pedido;
        }

        public IEnumerable<Pedido> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Pedido entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, Pedido entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
