using System;

namespace BooksWebApi.Models.Models
{
    public class PedidoDto
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int LibroId { get; set; }
        public string Libro { get; set; }
        public DateTime Fecha { get; set; }
    }
}
