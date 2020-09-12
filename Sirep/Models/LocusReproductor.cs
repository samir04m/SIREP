using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class LocusReproductor
    {
        public int Id { get; set; }

        [Required]
        public int ReproductorId { get; set; }

        public int ValorA { get; set; }

        public int ValorB { get; set; }

        public int? Numero { get; set; }

        [ForeignKey("ReproductorId")]
        public virtual Reproductor Reproductor { get; set; }
    }
}
