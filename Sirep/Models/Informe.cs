using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Informe
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        public int CentroId { get; set; }

        [Required]
        public int EspecieId { get; set; }

        [ForeignKey("CentroId")]
        public virtual Centro Centro { get; set; }

        [ForeignKey("EspecieId")]
        public virtual Especie Especie { get; set; }
    }
}
