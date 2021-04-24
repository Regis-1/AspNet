using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Engine;
using WebApp.Data;

namespace WebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DetailsModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public GlobalDataBase GlobalDataBase { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GlobalDataBase = await _context.GlobalDataBase.FirstOrDefaultAsync(m => m.GlobalDataBaseId == id);

            if (GlobalDataBase == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
