using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mvc_Session_NetCore.DAOs;
using mvc_Session_NetCore.Models;

namespace mvc_Session_NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {

            MiPropioMain();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void MiPropioMain()
        {

            UsuarioDAO.getInstancia()
                .add(new Usuario("pepito", "123"))
                .add(new Usuario("123", "123"))
                .add(new Usuario("admin", "admin"));

        }
    }
}
