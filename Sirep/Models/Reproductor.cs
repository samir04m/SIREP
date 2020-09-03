using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Reproductor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ChipId { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Required]
        [Display(Name = "Lugar de procedencia")]
        [StringLength(100)]
        public string LugarProcedencia { get; set; }

        [Required]
        [Display(Name = "Especie")]
        public int EspecieId { get; set; }

        [Required]
        [Display(Name = "Lote")]
        public int LoteId { get; set; }

        [Display(Name = "Cuenca")]
        public int? CuencaId { get; set; }

        public virtual IEnumerable<DatosReproductor> Datos { get; set; }

        public virtual IEnumerable<ImagenReproductor> Imagenes { get; set; }

        [ForeignKey("EspecieId")]
        public virtual Especie Especie { get; set; }

        [ForeignKey("LoteId")]
        public virtual Lote Lote { get; set; }

        [ForeignKey("CuencaId")]
        public virtual Cuenca Cuenca { get; set; }

    }
}
