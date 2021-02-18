using System;

namespace BooksWebApi.Inputs
{
    public class PedidoInput
    {
        public int ClienteId { get; set; }
        public int LibroId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
