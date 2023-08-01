using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity2.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(50,ErrorMessage = "El {0} debe estar entre al menos {2} caracteres de longitud",MinimumLength = 5 )]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "La confirmacion de la contraseña es Obligatorio")]
        [Compare("Password", ErrorMessage = "La contraseña y confirmacion de contraseña no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
		[Display(Name = "Fecha de nacimiento")]
		public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio")]
        public bool Estado { get; set; }

    }
    
}
