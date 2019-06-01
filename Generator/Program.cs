using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.IO;


namespace HIAE.SIAF.Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //LerArquivoJson("teste.json");
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IActionResult LerArquivoJson(string arquivo)
        //{
        //    //var texto = System.IO.File.ReadAllText("My textfile.txt");

        //    using (StreamReader r = new StreamReader(arquivo))
        //    {
        //        string json = r.ReadToEnd();
        //        //dynamic array = serializer.DeserializeObject(json);
        //        Console.WriteLine("");
        //        //Console.WriteLine(serializer.Serialize(array));
        //        //Console.WriteLine("");
        //        return ok(json);
        //    }
        //}


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddOcelot()
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {
                s.AddOcelot();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseOcelot().Wait();
            });

    }


}
