using Engine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebConnector wc;
            DataBase context = new DataBase();
            wc = new WebConnector("https://api.covid19api.com/");

            DateTime today = DateTime.Today;
            bool existance = false;

            /*if (context.GDB.Any(record => record.DateDataBase == today)) existance = true;
            if (!existance)
            {
                wc.SetGlobalSummary();
                GlobalData gd1 = JsonParser.ExtractSingleData<GlobalData>(wc.Connect(), "Global");
                context.GDB.Add(new GlobalDataBase { TotalConfirmed = gd1.TotalConfirmed, DateDataBase = today });
                context.SaveChanges();

            }

            if (existance) Console.WriteLine("Todays data already in the data base.");
            else if (!existance) Console.WriteLine("Todays data added.");

            Console.WriteLine("\n***Global data checked!***\n");
            */
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
