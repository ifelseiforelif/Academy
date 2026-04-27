using Academy.Context;
using Academy.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Academy;

internal class Program
{
    static async Task Main(string[] args)
    {

        var services = new ServiceCollection(); //Створюється контейнер DI (Dependency Injection)

        services.AddDbContext<AcademyDbContext>(options =>
        {
            DbContextConfigurator.Configure(options);
        });
       
        

        services.AddScoped<GroupService>();
        services.AddScoped<Academy>(); // Academy теж реєструємо в DI
        //Створюється вже «працюючий» контейнер.
        var provider = services.BuildServiceProvider();

        //"Один DbContext на одну операцію роботи з БД"
    
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AcademyDbContext>();

        if (context.Database.CanConnect())
        {
            Console.WriteLine("Пiдключення до БД встановлено");
            var academy = scope.ServiceProvider.GetRequiredService<Academy>();
            //var result = await academy.AddGroup();
            await new Server().RunServer(academy);

        }
        else
        {
            Console.WriteLine("Не вдалось підключитись до БД");
        }
        
      
    }
}
