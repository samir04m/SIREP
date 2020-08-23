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
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NID { get; set; }
        public string IdUser { get; set; }
    }
}
