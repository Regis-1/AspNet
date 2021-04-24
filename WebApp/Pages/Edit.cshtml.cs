using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Engine;
using WebApp.Data;

namespace WebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public EditModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GlobalDataBase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalDataBaseExists(GlobalDataBase.GlobalDataBaseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GlobalDataBaseExists(int id)
        {
            return _context.GlobalDataBase.Any(e => e.GlobalDataBaseId == id);
        }
    }
}
