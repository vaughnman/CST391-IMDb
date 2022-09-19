using Databases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var ratingCalculator = ServiceDescriptor.Describe(typeof(IRatingCalculator), typeof(RatingCalculator), ServiceLifetime.Singleton);
builder.Services.Add(ratingCalculator);


//Stubs
var reviewDatabase = ServiceDescriptor.Describe(typeof(IReviewDatabase), typeof(StubReviewDatabase), ServiceLifetime.Singleton);
var albumDatabase = ServiceDescriptor.Describe(typeof(IAlbumDatabase), typeof(StubAlbumDatabase), ServiceLifetime.Singleton);

builder.Services.Add(reviewDatabase);
builder.Services.Add(albumDatabase);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "album",
    pattern: "{controller=Album}/{action=Index}/{id?}");
    
app.MapControllerRoute(
    name: "review",
    pattern: "{controller=Review}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "rating",
    pattern: "{controller=Rating}/{action=Index}/{id?}");

app.Run();
