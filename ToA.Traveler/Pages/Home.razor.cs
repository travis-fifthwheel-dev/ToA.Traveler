using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Pages;

public class HomeBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    protected IActionSubscriber ActionSubscriber { get; set; } = default!;

    protected int Day => State.Value.Day;
    protected bool IsCamping => State.Value.IsCamping;
    protected bool IsNavigating => State.Value.IsNavigating;
    protected bool IsDayEnded => State.Value.IsDayEnded;
    protected bool IsTraveling => State.Value.IsTraveling;
    protected bool IsConfirmingTravelJobs => State.Value.IsConfirmingTravelJobs;
    protected bool IsConfirmingTravelSpecs => State.Value.IsConfirmingTravelSpecs;

    protected void StartTravel() => Dispatcher.Dispatch(new TravelActions.StartTravel());
}