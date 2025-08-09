using System.ComponentModel.DataAnnotations;

namespace LosNeteros.Models
{
    public class ClienteBCP
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener 8 dígitos.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El número de cuenta es obligatorio.")]
        [RegularExpression(@"^\d{10,20}$", ErrorMessage = "La cuenta debe tener entre 10 y 20 dígitos.")]
        public string NumeroCuenta { get; set; }
    }
}