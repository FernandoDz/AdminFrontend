using System.ComponentModel.DataAnnotations;

namespace FrontenAdministracion.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Rol es obligatorio")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Login es obligatorio")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Estatus es obligatorio")]
        public int Estatus { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria")]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y confirmar password deben de ser iguales")]
        [Display(Name = "Confirmar password")]
        public string ConfirmPassword_aux { get; set; }
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "El Login es obligatorio")]
        public string Login { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }

}
