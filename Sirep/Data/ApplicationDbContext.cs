using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sirep.Areas.Admin.Models;
using Sirep.Models;

namespace Sirep.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TUsuarios> Usuarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<RepresentanteLegal> RepresentantesLegales { get; set; }
        public DbSet<Centro> Centros { get; set; }
        public DbSet<CentroUsuario> CentroUsuarios { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisoCentro> PermisoCentros { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Cuenca> Cuencas { get; set; }
        public DbSet<Reproductor> Reproductores { get; set; }
        public DbSet<DatosReproductor> DatosReproductores { get; set; }
        public DbSet<ImagenReproductor> ImagenReproductores { get; set; }

    }
}
