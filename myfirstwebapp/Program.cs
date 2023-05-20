using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace myfirstwebapp
{
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
                    webBuilder.UseUrls("http://0.0.0.0:5000");
                    webBuilder.ConfigureKestrel((hostingContext, options) =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;
                        options.Limits.MinResponseDataRate = null;
                        options.Limits.MaxRequestBodySize = 1048576; // 1MB
                        options.Limits.MaxRequestBufferSize = 1048576; // 1MB
                        options.Limits.MaxResponseBufferSize = 1048576; // 1MB
                        options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(60);
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
                        
                    });
                });
    }
}

