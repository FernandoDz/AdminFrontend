using System.ComponentModel.DataAnnotations;

namespace FrontenAdministracion.Models
{
    public class Municipio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El departamento es requerido")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La imagen es requerida")]
        public string Imagen { get; set; }

        public Departamento Departamento { get; set; }
    }
}
