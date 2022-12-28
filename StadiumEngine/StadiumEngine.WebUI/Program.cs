using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace StadiumEngine.WebUI
{
    /// <summary>
    /// Точка входа
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        public static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Logging Process Error: {msg}"));
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"))
                .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command"))
                .WriteTo.Console()
                .WriteTo.PostgreSQL(connectionString: Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"),
                    tableName: "PUBLIC.LOG_ERRORS",
                    needAutoCreateTable: true,
                    restrictedToMinimumLevel: LogEventLevel.Error)
                .CreateLogger();
            
            CreateHostBuilder(args).Build().Run();
        }
        
        /// <summary>
        /// Билд приложения
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}