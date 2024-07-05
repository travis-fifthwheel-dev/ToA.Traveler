using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.TropicalStormOutcomeComponent;

public class TropicalStormOutcomeBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected IEnumerable<Services.TropicalStormOutcome>? TropicalStormOutcomes => State.Value.TropicalStormOutcomes;
    protected bool IsTropicalStorm => State.Value.WeatherReport?.IsTropicalStorm ?? false;
}
