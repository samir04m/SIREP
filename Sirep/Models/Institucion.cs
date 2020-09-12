using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Institucion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(300)]
        [Display(Name = "Información")]
        public string Informacion { get; set; }

        public virtual IEnumerable<PermisoCentro> PermisoCentros { get; set; }
    }
}
