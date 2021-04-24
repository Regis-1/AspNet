using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages
{
    public class GlobalView : PageModel
    {

        private WebConnector wc;
        private readonly WebApp.Data.WebAppContext _context;

        public GlobalView(WebApp.Data.WebAppContext context)
        {
            _context = context;
            WebApi();
        }

        public IList<GlobalDataBase> GlobalDataBase { get; set; }

        public async Task OnGetAsync()
        {
            GlobalDataBase = await _context.GlobalDataBase.ToListAsync();
        }

        private void WebApi()
        {
            //Creating webconnector
            wc = new WebConnector("https://api.covid19api.com/");

            var globalDataSets = (from s in _context.GlobalDataBase select s).ToList<GlobalDataBase>();

            DateTime today = DateTime.Today;
            bool existance = false;

            if (_context.GlobalDataBase.Any(record => record.DateDataBase == today)) existance = true;
            if (!existance)
            {
                wc.SetGlobalSummary();
                GlobalData gd1 = JsonParser.ExtractSingleData<GlobalData>(wc.Connect(), "Global");
                _context.GlobalDataBase.Add(new GlobalDataBase { TotalConfirmed = gd1.TotalConfirmed, DateDataBase = today });
                _context.SaveChanges();

            }

            if (existance) Console.WriteLine("Todays data already in the data base.");
            else if (!existance) Console.WriteLine("Todays data added.");

            Console.WriteLine("\n***Global data checked!***\n");
        }

    }
}
