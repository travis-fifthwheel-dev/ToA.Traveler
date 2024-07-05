using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Models;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.RationWaterComponent;

public class RationWaterBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    protected ILogger<RationWaterBase> Logger { get; set; } = default!;

    protected decimal RequiredWater => 
        State.Value.WeatherReport is null ? 2M : (State.Value.WeatherReport.Temperature == Temperature.ExtremeHeat ? 3M : 2M);

    protected void Food(CharacterName characterName)
    {
        Console.WriteLine($"Dispatching ConsumeFood for {characterName}");
        Dispatcher.Dispatch(new TravelActions.ConsumeFood(characterName));
    }

    protected void Water(CharacterName characterName)
    {
        Console.WriteLine($"Dispatching ConsumeWater for {characterName}");
        Dispatcher.Dispatch(new TravelActions.ConsumeWater(characterName));
    }
}