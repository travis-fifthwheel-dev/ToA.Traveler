using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Services;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.NavigatorOutcomeComponent;

public class NavigatorOutcomeBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected NavigationOutcome? NavigationOutcome => State.Value.NavigationOutcome;
}
