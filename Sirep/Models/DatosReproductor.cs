using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class DatosReproductor
    {
        public int Id { get; set; }

        [Required]
        public int ReproductorId { get; set; }

        public double Peso { get; set; }

        public double Talla { get; set; }

        public bool Sexo { get; set; }

        public double HTO { get; set; }

        public double HTOLab { get; set; }

        public double HB { get; set; }

        public double PPT { get; set; }

        public double CSL { get; set; }

        public double PS { get; set; }

        public double UREA { get; set; }

        public double GLS { get; set; }

        public double RBC { get; set; }

        public double WBC { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        [StringLength(100)]
        public string Ojos { get; set; }

        [StringLength(100)]
        public string Piel { get; set; }

        [StringLength(100)]
        public string Aleta { get; set; }

        [StringLength(100)]
        public string Branquias { get; set; }

        [ForeignKey("ReproductorId")]
        public virtual Reproductor Reproductor { get; set; }
    }
}
