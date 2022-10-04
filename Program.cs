using Databases;
using DotNetEnv;

Env.Load();

var imdbBuilder = CreateBuilderWithServices();
var imdb = CreateAppWithRoutes(imdbBuilder);

imdb.UseMiddleware(typeof(CorsMiddleware));

imdb.Run();

WebApplicationBuilder CreateBuilderWithServices()
{
    var builder = WebApplication.CreateBuilder();
    builder.Services.AddControllers();
    ConfigureServices(builder);
    return builder;
}

WebApplication CreateAppWithRoutes(WebApplicationBuilder builder)
{
    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
        app.UseHsts();

    app.UseHttpsRedirection();

    app.UseRouting();

    AddRoutes(app);

    return app;
}

void AddRoutes(WebApplication app)
{
    app.MapControllerRoute(
        name: "album",
        pattern: "{controller=Album}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "review",
        pattern: "{controller=Review}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "rating",
        pattern: "{controller=Rating}/{action=Index}/{id?}");
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var ratingCalculator = ServiceDescriptor.Describe(typeof(IRatingCalculator), typeof(RatingCalculator), ServiceLifetime.Scoped);
    builder.Services.Add(ratingCalculator);

    if(HasConnectionString())
    {
        Console.WriteLine("Using Live Database");
        var reviewDatabase = ServiceDescriptor.Describe(typeof(IReviewDatabase), typeof(ReviewDatabase), ServiceLifetime.Scoped);
        var albumDatabase = ServiceDescriptor.Describe(typeof(IAlbumDatabase), typeof(AlbumDatabase), ServiceLifetime.Scoped);
    
        builder.Services.Add(reviewDatabase);
        builder.Services.Add(albumDatabase);
    }
    else
    {
        Console.WriteLine("Using Stub Database");
        var reviewDatabase = ServiceDescriptor.Describe(typeof(IReviewDatabase), typeof(StubReviewDatabase), ServiceLifetime.Singleton);
        var albumDatabase = ServiceDescriptor.Describe(typeof(IAlbumDatabase), typeof(StubAlbumDatabase), ServiceLifetime.Singleton);
    
        builder.Services.Add(reviewDatabase);
        builder.Services.Add(albumDatabase);
    }
}

bool HasConnectionString()
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
    return connectionString != null && connectionString != "";
}