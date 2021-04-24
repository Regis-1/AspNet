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
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public IList<GlobalDataBase> GlobalDataBase { get;set; }

        public async Task OnGetAsync()
        {
            GlobalDataBase = await _context.GlobalDataBase.ToListAsync();
        }
    }
}
