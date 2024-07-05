using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Services;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.WeatherReportComponent;

public class WeatherReporterBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> TravelState { get; set; } = default!;

    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    protected WeatherReport? WeatherReport => TravelState.Value.WeatherReport;

    protected void RollWeather() => Dispatcher.Dispatch(new TravelActions.ReportWeather());
}
