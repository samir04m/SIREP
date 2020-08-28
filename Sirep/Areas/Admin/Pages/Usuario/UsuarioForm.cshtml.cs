using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sirep.Areas.Admin.Models;
using Sirep.Data;
using Sirep.Library;

namespace Sirep.Areas.Admin.Pages.Usuario
{
    public class UsuarioFormModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private LUsuariosRoles _userRoles;
        private static InputModel _dataInput;
        private IWebHostEnvironment _environment;
        private static UsuarioInputModel _dataUser1, _dataUser2;
        private static int idLastUserCreated = 0;

        public UsuarioFormModel (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _environment = environment;
            _userRoles = new LUsuariosRoles();
        }


        public void OnGet(int id)
        {
            _dataUser2 = null;  
            
            if (_dataInput != null || _dataUser1 != null || _dataUser2 != null)
            {
                if (_dataInput != null)
                {
                    Input = _dataInput;
                    Input.rolesLista = _userRoles.getRoles(_roleManager);
                }
                else
                {
                    if (_dataUser1 != null || _dataUser2 != null)
                    {
                        if (_dataUser2 != null) _dataUser1 = _dataUser2;
                        Input = new InputModel
                        {
                            Id = _dataUser1.Id,
                            Name = _dataUser1.Name,
                            LastName = _dataUser1.LastName,
                            NID = _dataUser1.NID,
                            Email = _dataUser1.IdentityUser.Email,
                            PhoneNumber = _dataUser1.IdentityUser.PhoneNumber,
                            rolesLista = getRoles(_dataUser1.Role),
                        };
                        if (_dataInput != null)
                        {
                            Input.ErrorMessage = _dataInput.ErrorMessage;
                        }
                    }
                }
            }
            else
            {
                Input = new InputModel
                {
                    rolesLista = _userRoles.getRoles(_roleManager)
                };
            }
            _dataUser2 = _dataUser1;
            _dataUser1 = null;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : UsuarioInputModel
        {
            [TempData]
            public string ErrorMessage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }

        public async Task<IActionResult> OnPost(String dataUser)
        {
            if (dataUser == null)
            {
                if (_dataUser2 == null)
                {
                    if (await SaveAsync())
                    {
                        if (!idLastUserCreated.Equals(0))
                        {
                            var url = $"/Admin/Usuario/Details?id={idLastUserCreated}";
                            idLastUserCreated = 0;
                            return Redirect(url);
                        }
                        return Redirect("/Admin/Usuarios");
                    }
                    else
                    {
                        return Redirect("/Admin/Usuarios/Form");
                    }
                }
                else
                {
                    if (await UpdateAsync())
                    {
                        var url = $"/Admin/Usuario/Details?id={_dataUser2.Id}";
                        _dataUser2 = null;
                        return Redirect(url);
                    }
                    else
                    {
                        return Redirect("/Admin/Usuarios");
                    }
                }
            }
            else
            {
                _dataUser1 = JsonConvert.DeserializeObject<UsuarioInputModel>(dataUser);
                return Redirect("/Admin/Usuarios/Form?id=1");
            }
        }

        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            var valor = false;
            if (ModelState.IsValid)
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count.Equals(0))
                {
                    var strategy = _context.Database.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () => {
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                var user = new IdentityUser
                                {
                                    UserName = Input.Email,
                                    Email = Input.Email,
                                    PhoneNumber = Input.PhoneNumber
                                };
                                var result = await _userManager.CreateAsync(user, Input.Password);
                                if (result.Succeeded)
                                {
                                    await _userManager.AddToRoleAsync(user, Input.Role);
                                    var dataUser = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList().Last();
                                    var t_usuario = new TUsuarios
                                    {
                                        Nombre = Input.Name,
                                        Apellido = Input.LastName,
                                        NID = Input.NID,
                                        IdUser = dataUser.Id,
                                    };
                                    await _context.AddAsync(t_usuario);
                                    _context.SaveChanges();
                                    transaction.Commit();
                                    idLastUserCreated = t_usuario.ID;
                                    _dataInput = null;
                                    valor = true;
                                }
                                else
                                {
                                    foreach (var item in result.Errors)
                                    {
                                        _dataInput.ErrorMessage = item.Description;
                                    }
                                    valor = false;
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
                else
                {
                    _dataInput.ErrorMessage = $"El {Input.Email} ya esta registrado";
                    valor = false;
                }
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _dataInput.ErrorMessage += error.ErrorMessage;
                    }
                }
                valor = false;
            }

            return valor;
        }

        private List<SelectListItem> getRoles(String role)
        {
            List<SelectListItem> rolesLista = new List<SelectListItem>();
            rolesLista.Add(new SelectListItem
            {
                Text = role
            });
            var roles = _userRoles.getRoles(_roleManager);
            roles.ForEach(item => {
                if (item.Text != role)
                {
                    rolesLista.Add(new SelectListItem
                    {
                        Text = item.Text
                    });
                }
            });
            return rolesLista;
        }

        private async Task<bool> UpdateAsync()
        {
            var valor = false;
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () => {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var identityUser = _userManager.Users.Where(u => u.Id.Equals(_dataUser2.ID)).ToList().Last();
                        identityUser.UserName = Input.Email;
                        identityUser.Email = Input.Email;
                        identityUser.PhoneNumber = Input.PhoneNumber;
                        _context.Update(identityUser);
                        await _context.SaveChangesAsync();

                        var t_user = new TUsuarios
                        {
                            ID = _dataUser2.Id,
                            Nombre = Input.Name,
                            Apellido = Input.LastName,
                            NID = Input.NID,
                            IdUser = _dataUser2.ID,
                        };
                        _context.Update(t_user);
                        _context.SaveChanges();
                        if (_dataUser2.Role != Input.Role)
                        {
                            await _userManager.RemoveFromRoleAsync(identityUser, _dataUser2.Role);
                            await _userManager.AddToRoleAsync(identityUser, Input.Role);
                        }
                        transaction.Commit();
                        valor = true;
                    }
                    catch (Exception ex)
                    {

                        _dataInput.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        valor = false;
                    }
                }
            });
            return valor;
        }

    }
}
