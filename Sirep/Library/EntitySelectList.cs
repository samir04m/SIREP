using Microsoft.AspNetCore.Mvc.Rendering;
using Sirep.Data;
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
    }
}
