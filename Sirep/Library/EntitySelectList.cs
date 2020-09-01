using Microsoft.AspNetCore.Mvc.Rendering;
using Sirep.Areas.Admin.Models;
using Sirep.Data;
using Sirep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Library
{
    public class EntitySelectList
    {
        private ApplicationDbContext _context;

        public EntitySelectList(ApplicationDbContext context)
        {
            _context = context;
        }

        public static SelectListItem defaultOption = new SelectListItem
        {
                    Value = "0",
                    Text = "Seleccione una opción",
                    Selected = true,
                    Disabled = true
        };

        public List<SelectListItem> UsuariosSelectList(bool edit = false)
        {
            var selectList = new List<SelectListItem>();
            if (!edit) selectList.Add(defaultOption);
            var usuarios = _context.Usuarios.ToList();
            usuarios.ForEach(item => {
                selectList.Add(new SelectListItem
                {
                    Value = item.ID + "",
                    Text = item.Nombre+" "+ item.Apellido
                });
            });
            return selectList;
        }

        public List<SelectListItem> UsuariosCentroSelectList(IEnumerable<CentroUsuario> centroUsuarios)
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(defaultOption);
            var rolCentro = _context.Roles.Where(x => x.Name == "Centro").FirstOrDefault();
            var userRoles = _context.UserRoles.Where(x => x.RoleId == rolCentro.Id).ToList();

            foreach (var ur in userRoles)
            {
                var usuario = _context.Usuarios.Where(u => u.IdUser == ur.UserId).FirstOrDefault();
                var existe = centroUsuarios.Where(c => c.UsuarioId == usuario.ID).FirstOrDefault();
                if (existe == null)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = usuario.ID + "",
                        Text = usuario.Nombre + " " + usuario.Apellido
                    });
                }
            }
            return selectList;
        }

        public List<SelectListItem> DepartamentosSelectList(bool edit = false)
        {
            var selectList = new List<SelectListItem>();
            if (!edit) selectList.Add(defaultOption);
            var departamentos = _context.Departamentos.ToList();
            departamentos.ForEach(item => {
                selectList.Add(new SelectListItem
                {
                    Value = item.Id + "",
                    Text = item.Nombre
                });
            });
            return selectList;
        }

        public List<SelectListItem> RepresentantesSelectList(bool edit = false)
        {
            var selectList = new List<SelectListItem>();
            if (!edit) selectList.Add(defaultOption);
            var representantes = _context.RepresentantesLegales.ToList();
            representantes.ForEach(item => {
                selectList.Add(new SelectListItem
                {
                    Value = item.Id + "",
                    Text = item.Nombre
                });
            });
            return selectList;
        }

        public List<SelectListItem> PermisosSelectList(bool edit = false)
        {
            var selectList = new List<SelectListItem>();
            if (!edit) selectList.Add(defaultOption);
            var list = _context.Permisos.ToList();
            list.ForEach(item => {
                selectList.Add(new SelectListItem
                {
                    Value = item.Id + "",
                    Text = item.Nombre
                });
            });
            return selectList;
        }

        public List<SelectListItem> InstitucionesSelectList(bool edit = false)
        {
            var selectList = new List<SelectListItem>();
            if (!edit) selectList.Add(defaultOption);
            var list = _context.Instituciones.ToList();
            list.ForEach(item => {
                selectList.Add(new SelectListItem
                {
                    Value = item.Id + "",
                    Text = item.Nombre
                });
            });
            return selectList;
        }


    }
}
