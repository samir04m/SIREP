using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual IEnumerable<Centro> Centros { get; set; }
    }
}
