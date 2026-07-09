using MotorsportHub.Application.Interfaces;
using MotorsportHub.Application.Services;
using MotorsportHub.Infrastructure.Depots;
using MotorsportHub.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Composition root : on branche ici les implémentations d'infrastructure
// sur les ports définis dans la couche Application. Le passage à EF Core
// se fera en remplaçant les repositories en mémoire.
builder.Services.AddSingleton<IPlateauRepository, InMemoryPlateauRepository>();
builder.Services.AddSingleton<IDisciplineRepository, InMemoryDisciplineRepository>();
builder.Services.AddScoped<PlateauxService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
