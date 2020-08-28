using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sirep.Areas.Admin.Models;
using Sirep.Data;

namespace Sirep.Areas.Admin.Pages.Usuario
{
    public class UsuarioDeleteModel : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;
        private static UsuarioInputModel _user1, _user2;
        private TUsuarios tusuario;

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public UsuarioInputModel Data { get; set; }
        }

        public UsuarioDeleteModel (
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            if (_user1 == null) _user1 = new UsuarioInputModel();
        }

        public void OnGet()
        {
            Input = new InputModel
            {
                Data = _user1,
            };
            _user1 = new UsuarioInputModel();
        }

        public async Task<IActionResult> OnPost(String entidad_string = null, int confirmacion = 0)
        {
            if (entidad_string != null && _user1.Id.Equals(0))
            {
                _user1 = JsonConvert.DeserializeObject<UsuarioInputModel>(entidad_string);
                _user2 = _user1;
            }
            if (!confirmacion.Equals(0) && _user2 != null)
            {
                if (await DeleteAsync())
                {
                    return Redirect("/Admin/Usuarios");
                }
            }
            return Redirect("/Admin/Usuario/Delete");
        }

        private async Task<bool> DeleteAsync()
        {
            var respuesta = false;

            var entidad = _userManager.Users.Where(u => u.Email.Equals(_user2.IdentityUser.Email)).FirstOrDefault();
            if (entidad != null)
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = await _userManager.RemoveFromRoleAsync(entidad, _user2.Role);
                            if (result.Succeeded)
                            {
                                var result2 = await _userManager.DeleteAsync(entidad);
                                if (result2.Succeeded)
                                {
                                    tusuario = _context.Usuarios.Find(_user2.Id);
                                    _context.Usuarios.Remove(tusuario);
                                    _context.SaveChanges();
                                    transaction.Commit();
                                    _user1 = new UsuarioInputModel();
                                    _user2 = null;
                                    respuesta = true;
                                }
                                else
                                {
                                    transaction.Rollback();
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                });
            }
            return respuesta;
        }



    }
}
