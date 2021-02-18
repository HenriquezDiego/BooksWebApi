using System;

namespace BooksWebApi.Inputs
{
    public class ClienteInput
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
