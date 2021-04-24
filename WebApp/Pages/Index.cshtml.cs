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
    [BindProperties]
    public class IndexModel : PageModel
    {

        public string country { get;  set; }
        public DateTime dateFrom { get;  set; }
        public DateTime dateTo { get;  set; }
        private WebConnector wc;
        public void OnGet()
        {
            ViewData["confirmedCases"] = "Brak Danych";
        }

        public void OnPost()
        {
            ViewData["confirmedCases"] = $"Po kliknięciu {country}";

            wc = new WebConnector("https://api.covid19api.com/");
            DateTime t1 = dateFrom;
            DateTime t2 = dateTo;
            string manyDays = "";
            string countryOutput = country;
            countryOutput.ToLower();
            if (t1.Date == t2.Date)
            {
                wc.SetRecentTotalByCountry(countryOutput);
                CountryData cd2 = JsonParser.ExtractListData<CountryData>(wc.Connect())[0];
                ViewData["confirmedCases"] = "Potwierdzone przypadki: " + cd2.Confirmed.ToString();
            }
            else if (DateTime.Compare(t1, t2) < 0)
            {
                wc.SetPeriodTotalByCountry(country, t1, t2);
                List<CountryData> cdl = JsonParser.ExtractListData<CountryData>(wc.Connect());
                int _c = 0;
                DateTime tTemp = t1;
                foreach (CountryData cd in cdl)
                {
                    manyDays += tTemp.AddDays(_c).ToShortDateString() + ": " + cd.Confirmed.ToString()+ "\n";
                    _c++;
                }            
                ViewData["confirmedCases"] = "Potwierdzone przypadki od " + t1.ToShortDateString() + " do " + t2.ToShortDateString() + " :\n" + manyDays;

            }
            else
            {
                ViewData["confirmedCases"] = $"Data 'Od' jest późniejsza od daty 'Do'!";
            }
        }
        
    }
}
