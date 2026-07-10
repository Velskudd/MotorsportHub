using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MotorsportHub.Application.Interfaces;
using MotorsportHub.Application.Services;
using MotorsportHub.Infrastructure.Depots;
using MotorsportHub.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Composition root : on branche ici les implémentations d'infrastructure
// sur les ports définis dans la couche Application. Lorsque l'API existera,
// ces repositories en mémoire seront remplacés par des clients HTTP.
builder.Services.AddSingleton<IPlateauRepository, InMemoryPlateauRepository>();
builder.Services.AddSingleton<IDisciplineRepository, InMemoryDisciplineRepository>();
builder.Services.AddScoped<PlateauxService>();

await builder.Build().RunAsync();
