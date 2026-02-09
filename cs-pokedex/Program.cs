using cs_pokedex.Components;
using cs_pokedex.Utilities;
using Microsoft.EntityFrameworkCore;
using cs_pokedex.Data;
using cs_pokedex.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register AppState so all components share the same instance
builder.Services.AddSingleton<AppState>();

// Configure EF Core with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories/services
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

var app = builder.Build();

// Apply pending migrations at startup (ensure database created)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
