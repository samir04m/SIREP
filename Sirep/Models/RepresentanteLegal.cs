using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class RepresentanteLegal
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string NID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }

        public virtual IEnumerable<Centro> Centros { get; set; }
    }
}
