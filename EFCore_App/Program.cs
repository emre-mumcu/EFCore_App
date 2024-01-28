using EFCore_App.AppLib.Data;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddMvc().AddRazorRuntimeCompilation();

    {   // DbContext:

        // In this type of service registration, connection string is NOT provided to DI.
        // It must be provided in DbContext's OnConfiguring method.
        builder.Services.AddDbContext<SampleDbContext>();

        // In this type of service registration, connection string is provided to DI.
        // If DbContext is created by DI, connection string is present in the instance.
        // But if user manually creates DbContext, the connection string must also be provided in DbContext's OnConfiguring method
        // builder.Services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(connectionString: ""));

        // Manually            
        // builder.Services.AddScoped(x => { return new SampleDbContext(); });
    }

    var app = builder.Build();

    App.Instance.DataConfiguration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("data.json", true)
        .Build();

    App.Instance.WebHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();

    app.MapDefaultControllerRoute();

    app.Run();
}
catch (Exception ex)
{
    Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddMvc(); })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure((ctx, app) =>
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Application Error {ex?.Message} {ex?.InnerException?.Message}");
            });
        });
    }).Build().Run();
}




public sealed class App
{
    private static readonly Lazy<App> appInstance = new Lazy<App>(() => new App());

    public static App Instance { get { return appInstance.Value; } }

    private App() { }

    public IConfiguration? DataConfiguration { get; set; }

    public IWebHostEnvironment? WebHostEnvironment { get; set; }
}