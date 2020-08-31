using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Admin.Models
{
    public class UsuarioInputModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Número de Identificación")]
        public string NID { get; set; }

        [Display(Name = "Número de teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "El formato de télefono no es válido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Rol del usuario")]
        [Required(ErrorMessage = "Seleccione un rol para el usuario.")]
        public string Role { get; set; }

        public string ID { get; set; }

        public int Id { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}
