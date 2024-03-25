using Back_End.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameverse_Project_Backend.IntegrationTests
{

//    как писать  тесты синтаксис  у гпт
//а ожидаемые результаты нужно  с помощью сварега на ходу тестировать  и подстраивать может сразу  и изменять
//использовать кастомные  веб апп фактори и перенести туда  инициализационную логику
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApiDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApiDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDB");
                });

            });
        }
    }
}
