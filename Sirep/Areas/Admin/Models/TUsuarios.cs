using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sirep.Areas.Admin.Models
{
    public class TUsuarios
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }

        [StringLength(30)]
        public string NID { get; set; }

        [Required]
        [StringLength(450)]
        public string IdUser { get; set; }
    }
}
