using ChefBuddy.App.Data;
using ChefBuddy.App.Repositories;
using ChefBuddy.App.Services;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var conventionPack = new  ConventionPack {new CamelCaseElementNameConvention()};
ConventionRegistry.Register("camelCase", conventionPack, t => true);

var mongoDbConnectionString = builder.Configuration["ConnectionStrings:Mongo"];
builder.Services.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(MongoClientSettings.FromConnectionString(mongoDbConnectionString)));

// Radzen components
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

// Repositories
builder.Services.AddScoped<RecipeRepository>();

// Services
builder.Services.AddScoped<RecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();