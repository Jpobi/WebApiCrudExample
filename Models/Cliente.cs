using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace webAPICrud.Models
{
    
    public class Cliente{
        public long Id { get; set; }
        [Required(ErrorMessage = "Se requiere Nombre")]
        public required string Nombre { get; set; }= string.Empty;
        [Required(ErrorMessage = "Se requiere Apellido")]
        public required string Apellido { get; set; }= string.Empty;
        [FechaNacimientoValida(ErrorMessage = "La fecha de nacimiento debe ser anterior a la fecha actual.")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Se requiere Cuit")]
        [RegularExpression(@"^\d{2}-\d{8}-\d$", ErrorMessage = "Cuit inválido.")]
        public required string Cuit { get; set; }
        public string Domicilio { get; set; }= string.Empty;
        [Required(ErrorMessage = "Se requiere Celular")]
        public required string telefonoCelular { get; set; }
        /// <example>ejemplo@mail.com</example>
        [Required(ErrorMessage = "Se requiere Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido.")]
        public required string Email { get; set; }="ejemplo@mail.com";
        public class FechaNacimientoValidaAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime fechaNacimiento = (DateTime)value;
                    if (fechaNacimiento.CompareTo(DateTime.Now) >= 0)
                    {
                        return new ValidationResult("La fecha de nacimiento debe ser anterior a la fecha actual.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}