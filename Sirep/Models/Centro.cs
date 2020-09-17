using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Centro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(30)]
        [Display(Name = "NID")]
        public string NID { get; set; }

        [StringLength(30)]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Corregimiento / Vereda")]
        public string Lugar { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Información")]
        public string Informacion { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }

        [Display(Name = "Representante Legal")]
        public int RepresentanteLegalId { get; set; }

        [ForeignKey("DepartamentoId")]
        public virtual Departamento Departamento { get; set; }

        [ForeignKey("RepresentanteLegalId")]
        public virtual RepresentanteLegal RepresentanteLegal { get; set; }

        public virtual IEnumerable<Lote> Lotes { get; set; }

        public virtual IEnumerable<CentroUsuario> CentroUsuarios { get; set; }

        public virtual IEnumerable<PermisoCentro> PermisoCentros { get; set; }

        public virtual IEnumerable<Informe> Informes { get; set; }
    }
}
