using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sirep.Areas.Admin.Models;
using Sirep.Data;
using Sirep.Library;

namespace Sirep.Areas.Admin.Pages.Usuario
{
    public class UsuarioDetailsModel : PageModel
    {
        private LUsuario _user;

        public UsuarioDetailsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _user = new LUsuario(userManager, signInManager, roleManager, context);
        }

        public void OnGet(int id)
        {
            var data = _user.getUsuariosAsync(null, id);
            if (0 < data.Result.Count)
            {
                Input = new InputModel
                {
                    Data = data.Result.ToList().Last(),
                };
            }
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public UsuarioInputModel Data { get; set; }
        }
    }
}
