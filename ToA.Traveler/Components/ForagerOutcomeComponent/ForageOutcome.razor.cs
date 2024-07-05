using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Services;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.ForagerOutcomeComponent;

public class ForagerOutcomeBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected IEnumerable<Services.ForageOutcome>? ForageOutcomes => State.Value.ForageOutcomes;
}
