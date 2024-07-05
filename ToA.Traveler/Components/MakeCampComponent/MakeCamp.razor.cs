using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.MakeCampComponent;

public class MakeCampBase : FluxorComponent
{
    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    public void MakeCamp()
    {
        Dispatcher.Dispatch(new TravelActions.MakeCamp());
    }
}
