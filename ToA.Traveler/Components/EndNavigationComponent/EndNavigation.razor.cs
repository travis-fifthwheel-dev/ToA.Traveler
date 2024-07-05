using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.EndNavigationComponent;

public class EndNavigationBase : FluxorComponent
{
    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    public void End()
    {
        Dispatcher.Dispatch(new TravelActions.EndNavigation());
    }
}
