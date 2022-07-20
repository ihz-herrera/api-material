using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft",Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Error)
                .WriteTo.SQLite(@"D:\01.Entrenamiento\log\applog.db")
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Aplicacion Iniciada");
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex.Message);
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
