using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sirep.Areas.Admin.Models;
using Sirep.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Library
{
    public class LUsuario : ListObject
    {
        public LUsuario(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _usersRole = new LUsuariosRoles();
        }

        public async Task<List<UsuarioInputModel>> getUsuariosAsync(String valor, int id)
        {
            List<TUsuarios> listUser;
            List<SelectListItem> _listRoles;
            List<UsuarioInputModel> userList = new List<UsuarioInputModel>();
            if (valor == null && id.Equals(0))
            {
                listUser = _context.Usuarios.ToList();
            }
            else
            {
                if (id.Equals(0))
                {
                    listUser = _context.Usuarios.Where(u => u.NID.StartsWith(valor) || u.Nombre.StartsWith(valor) ||
                                u.Apellido.StartsWith(valor)).ToList();
                }
                else
                {
                    listUser = _context.Usuarios.Where(u => u.ID.Equals(id)).ToList();
                }
            }

            if (!listUser.Count.Equals(0))
            {
                foreach (var item in listUser)
                {
                    _listRoles = await _usersRole.getRole(_userManager, _roleManager, item.IdUser);
                    var user = _context.Users.Where(u => u.Id.Equals(item.IdUser)).ToList().Last();
                    userList.Add(new UsuarioInputModel
                    {
                        Id = item.ID,
                        ID = item.IdUser,
                        NID = item.NID,
                        Name = item.Nombre,
                        LastName = item.Apellido,
                        Role = _listRoles[0].Text,
                        IdentityUser = user
                    });
                }
            }
            return userList;
        }
    }
}
