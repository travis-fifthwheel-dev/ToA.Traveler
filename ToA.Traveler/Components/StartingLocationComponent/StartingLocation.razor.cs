using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Services;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.StartingLocationComponent;

public class StartingLocationBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected NavigationOutcome? NavigationOutcome => State.Value.PreviousNavigationOutcome;
}
