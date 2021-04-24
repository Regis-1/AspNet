using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class CountryData
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public long Confirmed { get; set; }
        public long Deaths { get; set; }
        public long Recovered { get; set; }
        public long Active { get; set; }
    }

    public class GlobalData
    {
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }
        public long NewDeaths { get; set; }
        public long TotalDeaths { get; set; }
        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }
    }
}
