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

        [Display(Name = "Peso (gr)")]
        public double Peso { get; set; }

        [Display(Name = "Talla (cm)")]
        public double Talla { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        public double CSL { get; set; }
        public double PPT { get; set; }
        public double PS { get; set; }
        public double UREA { get; set; }
        public double GLS { get; set; }


        public double? RBC { get; set; }

        [Display(Name = "RBC-2d")]
        public double? RBC2d { get; set; }
        
        public double HB { get; set; }

        public double HTO { get; set; }

        [Display(Name = "HTO (Lab)")]
        public double HTOLab { get; set; }


        public double? VCM { get; set; }
        public double? HCM { get; set; }
        public double? CHCM { get; set; }
        public double? WBC { get; set; }

        [StringLength(100)]
        public string Ojos { get; set; }

        [StringLength(100)]
        public string Piel { get; set; }

        [StringLength(100)]
        public string Aleta { get; set; }

        [StringLength(100)]
        public string Branquias { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        [ForeignKey("ReproductorId")]
        public virtual Reproductor Reproductor { get; set; }
    }
}
