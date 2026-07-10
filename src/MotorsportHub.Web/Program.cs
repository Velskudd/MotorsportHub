using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MotorsportHub.Application.Interfaces;
using MotorsportHub.Application.Services;
using MotorsportHub.Infrastructure.Depots;
using MotorsportHub.Infrastructure.Supabase;
using MotorsportHub.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Composition root : si Supabase est configuré (wwwroot/appsettings.json),
// le catalogue est lu depuis la base via son API REST ; sinon, repli sur
// les données embarquées. La clé « anon » est publique par conception :
// la base n'autorise que la lecture (Row Level Security).
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseAnonKey = builder.Configuration["Supabase:AnonKey"];

if (!string.IsNullOrWhiteSpace(supabaseUrl) && !string.IsNullOrWhiteSpace(supabaseAnonKey))
{
    builder.Services.AddSingleton(_ =>
    {
        var http = new HttpClient
        {
            BaseAddress = new Uri($"{supabaseUrl.TrimEnd('/')}/rest/v1/")
        };
        http.DefaultRequestHeaders.Add("apikey", supabaseAnonKey);
        http.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseAnonKey}");
        return new SupabaseCatalogue(http);
    });
    builder.Services.AddSingleton<IPlateauRepository>(sp => sp.GetRequiredService<SupabaseCatalogue>());
    builder.Services.AddSingleton<IDisciplineRepository>(sp => sp.GetRequiredService<SupabaseCatalogue>());
}
else
{
    builder.Services.AddSingleton<IPlateauRepository, InMemoryPlateauRepository>();
    builder.Services.AddSingleton<IDisciplineRepository, InMemoryDisciplineRepository>();
}

builder.Services.AddScoped<PlateauxService>();

await builder.Build().RunAsync();
