using System;

namespace BooksWebApi.WiewModels
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
