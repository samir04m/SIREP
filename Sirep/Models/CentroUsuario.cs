using Sirep.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class CentroUsuario
    {
        public int Id { get; set; }

        [Required]
        public int CentroId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("CentroId")]
        public virtual Centro Centro { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual TUsuarios Usuario { get; set; }
    }
}
