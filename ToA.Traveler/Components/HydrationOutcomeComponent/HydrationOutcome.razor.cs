using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.HydrationOutcomeComponent;

public class HydrationOutcomesBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected IEnumerable<Services.HydrationOutcome>? HydrationOutcomes => State.Value.HydrationOutcomes;
}
