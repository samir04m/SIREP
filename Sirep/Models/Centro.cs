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

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string Codigo { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Direccion { get; set; }

        [StringLength(100)]
        public string Lugar { get; set; }

        [StringLength(100)]
        public string Administrador { get; set; }

        [Required]
        public int DepartamentoId { get; set; }

        public int RepresentanteLegalId { get; set; }

        public virtual IEnumerable<Reproductor> Reproductores { get; set; }

        public virtual IEnumerable<PermisoCentro> PermisoCentros { get; set; } 

        [ForeignKey("DepartamentoId")]
        public virtual Departamento Departamento { get; set; }

        [ForeignKey("RepresentanteLegalId")]
        public virtual RepresentanteLegal RepresentanteLegal { get; set; }

    }
}
