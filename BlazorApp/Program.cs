using BlazorApp;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static readonly IConfiguration _configuration;
    static Program()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        CreateServiceProvider(builder.Services);

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        var app = builder.Build();

        app.UseRouting();
        app.UseStaticFiles();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection isc)
    {
        isc.AddSingleton(_configuration);

        var s = _configuration["Connnectionstrings:MySqlConnection"];
        isc.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(
                s,
                ServerVersion.AutoDetect(s),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                );
        });

        isc.AddScoped<DbContext, AppDbContext>();
        isc.AddScoped<DbService>();
    }

    public static ServiceProvider CreateServiceProvider(IServiceCollection isc)
    {
        ConfigureServices(isc);

        return isc.BuildServiceProvider();
    }
}