using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class PermisoCentro
    {
        public int Id { get; set; }

        [Required]
        public int CentroId { get; set; }

        [Required]
        public int InstitucionId { get; set; }

        [Required]
        public int PermisoId { get; set; }

        [ForeignKey("CentroId")]
        public virtual Centro Centro { get; set; }

        [ForeignKey("InstitucionId")]
        public virtual Institucion Institucion { get; set; }

        [ForeignKey("PermisoId")]
        public virtual Permiso Permiso { get; set; }

    }
}
