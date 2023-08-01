using Microsoft.AspNetCore.Identity;

namespace ProyectoIdentity2.Models
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre {get; set;}
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }

    }
}
