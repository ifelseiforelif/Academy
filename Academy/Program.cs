using Academy.Context;
using Academy.Entities;
using Academy.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace Academy;

internal class Program
{
    static async Task Main(string[] args)
    {

        var services = new ServiceCollection(); //Створюється контейнер DI (Dependency Injection)

        //Конфігуратор
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        //Реєстрація AcademyDbContext у DI
        services.AddDbContext<AcademyDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("MSSQLConnection")));

        services.AddScoped<GroupService>();
        services.AddScoped<Academy>(); // Academy теж реєструємо в DI
        //Створюється вже «працюючий» контейнер.
        var provider = services.BuildServiceProvider();

        //"Один DbContext на одну операцію роботи з БД"
    
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AcademyDbContext>();

        if (await context.Database.CanConnectAsync())
        {
            Console.WriteLine("Пiдключення до БД встановлено");
        }
        else
        {
            Console.WriteLine("Не вдалось підключитись до БД");
        }
        //var academy = scope.ServiceProvider.GetRequiredService<Academy>();
        //var result = await academy.AddGroup();
    }
}
