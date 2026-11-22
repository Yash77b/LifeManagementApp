using LifeManagementApp.Data;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;
using LifeManagementApp.Views;
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

            // Database: 
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "todos.db");
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlite($"Data Source={dbPath}"),
                ServiceLifetime.Singleton);

            // Services
            builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
            builder.Services.AddSingleton<HttpClient>();                                  
            builder.Services.AddSingleton<IWeatherService, OpenMeteoWeatherService>();    

            // ViewModels and pages
            builder.Services.AddTransient<TodoListViewModel>();
            builder.Services.AddTransient<TodoListPage>();
            builder.Services.AddTransient<AddTodoViewModel>();
            builder.Services.AddTransient<AddTodoPage>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<EditTodoViewModel>();
            builder.Services.AddTransient<EditTodoPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            app.Services.GetRequiredService<AppDbContext>().Database.EnsureCreated();

            return app;
        }
    }
}