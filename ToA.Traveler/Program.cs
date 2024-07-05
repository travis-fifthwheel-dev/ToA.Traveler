using Blazored.LocalStorage;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ToA.Traveler;
using ToA.Traveler.Services;
using ToA.Traveler.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddFluxor(config =>
{
    config.ScanAssemblies(typeof(Program).Assembly);
    config.UseReduxDevTools();
    config.AddMiddleware<StatePersistenceMiddleware>();
});

builder.Services.AddScoped<StatePersistenceService>();
builder.Services.AddTransient<IDiceRoller, DiceRoller>();
builder.Services.AddTransient<IWeatherTable, WeatherTable>();
builder.Services.AddTransient<INavigatorService, NavigatorService>();
builder.Services.AddTransient<IForagerService, ForagerService>();
builder.Services.AddTransient<IEncounterService, EncounterService>();
builder.Services.AddTransient<IHydrationService, HydrationService>();
builder.Services.AddTransient<IStarvationService, StarvationService>();
builder.Services.AddTransient<ITropicalStormService, TropicalStormService>();

await builder.Build().RunAsync();
