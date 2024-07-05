using Fluxor.Blazor.Web.Components;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.StarvationOutcomeComponent;

public class StarvationOutcomeBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected IEnumerable<Services.StarvationOutcome>? StarvationOutcomes => State.Value.StarvationOutcomes;
}