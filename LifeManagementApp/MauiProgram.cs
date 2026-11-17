using LifeManagementApp.Data;
using LifeManagementApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Database: todos.db file in data folder 
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "todos.db");
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlite($"Data Source={dbPath}"),
                ServiceLifetime.Singleton);

            // Services
            builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Database/TodoItem create from class
            app.Services.GetRequiredService<AppDbContext>().Database.EnsureCreated();

            return app;
        }
    }
}