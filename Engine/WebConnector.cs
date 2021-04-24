using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace Engine
{
    public class WebConnector
    {
        private string baseUrl;
        private string connectUrl = "";

        public WebConnector(string bUrl)
        {
            baseUrl = bUrl;
        }

        /*
         * Set connection path and parameters. Needed for changing path to connect or filter values.
         * Function constructs "connectUrl" for later usage.
         * 
         * path -> path to specific file eg. /total/poland/
         * parameters -> parameters for HTTPS GET method eg. ...?status=confirmed
         */
        private void SetConnection(string path, Dictionary<string, string> parameters)
        {
            connectUrl = baseUrl + path + "?";
            foreach (KeyValuePair<string,string> kvp in parameters)
            {
                connectUrl += kvp.Key+"="+kvp.Value+"&";
            }
            connectUrl = connectUrl.Remove(connectUrl.Length - 1, 1);
        }

        private void SetConnection(string path)
        {
            connectUrl = baseUrl + path;
        }

        /*
         * Sets the connector for getting data about the newest total data of given country
         * 
         * country -> written in english name of a country eg. "poland", "france" etc.
         */
        public void SetRecentTotalByCountry(string country)
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime dayBefore = yesterday.AddDays(-1);
            SetPeriodTotalByCountry(country, dayBefore, yesterday);
        }

        /*
         * Sets the connector for getting list of total data about given country
         * for given period between two dates. t1 is not included in results!
         */
        public void SetPeriodTotalByCountry(string country, DateTime t1, DateTime t2)
        {
            string ft1 = FormatDate(t1);
            string ft2 = FormatDate(t2);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("from", ft1);
            parameters.Add("to", ft2);

            SetConnection("total/country/" + country, parameters);
        }

        public void SetGlobalSummary()
        {
            SetConnection("summary");
        }

        /*
         * Connect to set url. Function returns response string.
         * Response string can be used for JSON deserialization.
         */
        public string Connect()
        {
            var response = ConnectAsync();
            string r = response.Result.ToString();

            return r;
        }

        private async System.Threading.Tasks.Task<string> ConnectAsync()
        {
            Console.WriteLine("Connecting to " + connectUrl);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(connectUrl);
            string r = await response.Content.ReadAsStringAsync();
            return r;
        }

        /*
         * Function used only for returning formatted data accepted by API
         */
        private string FormatDate(DateTime d) => d.ToString("yyyy-MM-dd") + "T01:00:00Z";
    }
}
