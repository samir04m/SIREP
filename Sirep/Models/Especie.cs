using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Especie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Familia { get; set; }

        [Required]
        [StringLength(50)]
        public string Orden { get; set; }

        [StringLength(50)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Común")]
        public string NombreComun { get; set; }

        public virtual IEnumerable<Reproductor> Reproductores { get; set; }
    }
}
