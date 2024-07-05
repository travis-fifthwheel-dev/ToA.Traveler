using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.StartDayComponent;

public class StartDayBase : FluxorComponent
{
    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    protected void Click() => Dispatcher.Dispatch(new TravelActions.DayStarted());
}
