using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TestWebApp
{

    /// <summary>
    /// https://guestservices-sreint-uw-fa-sb.azurewebsites.net/api/Employee
    /// https://guestservices-sreint-uw-fa-sb.azurewebsites.net/api/Department
    /// </summary>


    public class Program
    {
        public static void Main(string[] args)
        {
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
