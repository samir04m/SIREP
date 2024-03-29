﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Models
{
    public class Cuenca
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual IEnumerable<Reproductor> Reproductores { get; set; }
    }
}
