using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Lote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Detalles { get; set; }

        [Required]
        public int CentroId { get; set; }

        [ForeignKey("CentroId")]
        public virtual Centro Centro { get; set; }

        public virtual IEnumerable<Reproductor> Reproductores { get; set; }
    }
}
