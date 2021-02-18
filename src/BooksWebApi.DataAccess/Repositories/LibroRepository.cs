using BooksWebApi.DataAccess.Data;
using BooksWebApi.Models.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BooksWebApi.DataAccess.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly IConnectionService _connectionService;

        public LibroRepository(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public Libro Get(int id)
        {
            var sqlCommand = new SqlCommand($"Select * from Libros where LibroId={id}", _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var libro = new Libro();

            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    libro = new Libro
                    {
                        LibroId = int.Parse(reader[0]?.ToString() ?? "0"),
                        Titulo = reader[1].ToString(),
                        Autor =  reader[2].ToString(),
                        Editorial = reader[3].ToString()
                    };
                }
            }

            _connectionService.Close();
            return libro;
        }

        public IEnumerable<Libro> GetAll()
        {
            var sqlCommand = new SqlCommand(@"Select * from Libros",
                _connectionService.GetConnection())
            {
                CommandType = CommandType.Text
            };
            _connectionService.Open();

            var libros = new List<Libro>();

            using(var reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    libros.Add(new Libro
                    {
                        LibroId = int.Parse(reader[0]?.ToString() ?? "0"),
                        Titulo = reader[1].ToString(),
                        Autor =  reader[2].ToString(),
                        Editorial = reader[3].ToString()
                    });
                }
            }

            _connectionService.Close();
            return libros;
        }

        public bool Insert(Libro entity)
        {
            var command = new SqlCommand
            {
                CommandText = "INSERT INTO Libros VALUES(@p1,@p2,@p3,@p4)"
            };

            var sqlCommand = new SqlCommand("select top(1) LibroId from Libros order by LibroId DESC", _connectionService.GetConnection())
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
            command.Parameters.AddWithValue("@p2", entity.Titulo);
            command.Parameters.AddWithValue("@p3", entity.Autor);
            command.Parameters.AddWithValue("@p3", entity.Editorial);
            command.Connection = _connectionService.GetConnection();


            var executeNonQuery = command.ExecuteNonQuery();
            _connectionService.Close();

            return executeNonQuery > 0;
        }

        public bool Delete(int id)
        {
            var sql = $"DELETE FROM Libros WHERE LibroId={id}";

            var cmd = new SqlCommand(sql, _connectionService.GetConnection()) {CommandType = CommandType.Text};
            _connectionService.Open();

            var result = cmd.ExecuteNonQuery();
            return result > 0;
        }

        public bool Update(int id, Libro entity)
        {
            var command = new SqlCommand
            {
                CommandText = $"INSERT INTO Libros VALUES(@p2,@p3,@p4) WHERE LibroId={id}"
            };

            command.Parameters.AddWithValue("@p2", entity.Titulo);
            command.Parameters.AddWithValue("@p3", entity.Autor);
            command.Parameters.AddWithValue("@p3", entity.Editorial);
            var result = command.ExecuteNonQuery();
            return result > 0;
        }
    }
}
