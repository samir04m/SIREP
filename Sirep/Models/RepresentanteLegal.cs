using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class RepresentanteLegal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string NID { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "El formato de correo no es valido.")]
        public string Email { get; set; }

        [StringLength(30)]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        public virtual IEnumerable<Centro> Centros { get; set; }
    }
}
