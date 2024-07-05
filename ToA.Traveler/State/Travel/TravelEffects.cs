using Fluxor;
using ToA.Traveler.Services;
using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.State.Travel;

public class TravelEffects(
    ILogger<TravelEffects> logger,
    IState<TravelState> state,
    IWeatherTable weatherTable,
    INavigatorService navigatorService,
    IForagerService foragerService,
    IEncounterService encounterService,
    IHydrationService hydrationService,
    IStarvationService starvationService,
    ITropicalStormService tropicalStormService)
{
    private readonly ILogger<TravelEffects> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IState<TravelState> _state = state ?? throw new ArgumentNullException(nameof(state));
    private readonly IWeatherTable _weatherTable = weatherTable ?? throw new ArgumentNullException(nameof(weatherTable));
    private readonly INavigatorService _navigatorService = navigatorService ?? throw new ArgumentNullException(nameof(navigatorService));
    private readonly IForagerService _foragerService = foragerService ?? throw new ArgumentNullException(nameof(foragerService));
    private readonly IEncounterService _encounterService = encounterService ?? throw new ArgumentNullException(nameof(encounterService));
    private readonly IHydrationService _hydrationService = hydrationService ?? throw new ArgumentNullException(nameof(hydrationService));
    private readonly IStarvationService _starvationService = starvationService ?? throw new ArgumentNullException(nameof(starvationService));
    private readonly ITropicalStormService _tropicalStormService = tropicalStormService ?? throw new ArgumentNullException(nameof(tropicalStormService));


    [EffectMethod]
    public Task Handle(
        TravelActions.DayStarted action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.ReportWeather());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.ReportWeather action,
        IDispatcher dispatcher)
    {
        var weatherReport = _weatherTable.Roll();
        dispatcher.Dispatch(new TravelActions.WeatherReported(weatherReport));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.StartTravel action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.TravelStarted());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.TravelStarted action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.ConfirmTravelJobs());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.TravelJobsConfirmed action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.ConfirmTravelSpecs());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.TravelSpecsConfirmed action,
        IDispatcher dispatcher)
    {
        if (_state.Value.WeatherReport is null)
            throw new Exception("Missing weather report.");

        if (_state.Value.Navigator is null)
            throw new Exception("Missing party navigator.");

        dispatcher.Dispatch(new TravelActions.StartNavigation(
            _state.Value.WeatherReport,
            _state.Value.Navigator,
            _state.Value.Cartographers,
            action.TravelPace,
            action.TravelTerrain,
            action.TravelDirection,
            UsingCanoe: false));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.StartNavigation action,
        IDispatcher dispatcher)
    {
        var outcome = _navigatorService.Navigate(
            action.Weather, 
            action.Navigator, 
            action.Cartographers,
            action.Pace, 
            action.Terrain, 
            action.Direction, 
            usingCanoe: false);

        dispatcher.Dispatch(new TravelActions.NavigationStarted(outcome));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.NavigationStarted action,
        IDispatcher dispatcher)
    {
        if(_state.Value.WeatherReport is null)
            throw new Exception("Missing weather report.");

        dispatcher.Dispatch(new TravelActions.RollEncounters(
            _state.Value.Foragers,
            _state.Value.Scouts,
            _state.Value.Cartographers,
            _state.Value.Navigator!,
            _state.Value.TravelPace,
            _state.Value.TravelTerrain));

        dispatcher.Dispatch(new TravelActions.StartForaging(
            _state.Value.Foragers, 
            _state.Value.TravelPace, 
            _state.Value.TravelTerrain, 
            _state.Value.WeatherReport));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.RollEncounters action,
        IDispatcher dispatcher)
    {
        var outcomes = _encounterService.Encounter(
            action.Foragers,
            action.Scouts,
            action.Cartographers,
            action.Navigator,
            action.Pace,
            action.Terrain);

        dispatcher.Dispatch(new TravelActions.EncountersRolled(outcomes));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.StartForaging action,
        IDispatcher dispatcher)
    {
        Console.WriteLine("Handling StartForaging action");
        var outcomes = _foragerService.Forage(
            action.Foragers,
            action.Pace,
            action.Terrain,
            action.Weather);

        dispatcher.Dispatch(new TravelActions.ForagingStarted(outcomes));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.EndNavigation action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.CheckHydration());

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.CheckHydration action,
        IDispatcher dispatcher)
    {
        if (_state.Value.WeatherReport is null)
            throw new Exception("Missing weather report.");

        var outcomes = _hydrationService.CheckHydration(_state.Value.TheParty, _state.Value.WeatherReport, _state.Value.TravelPace);
        dispatcher.Dispatch(new TravelActions.HydrationChecked(outcomes));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.HydrationChecked action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.CheckStarvation());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.CheckStarvation action,
        IDispatcher dispatcher)
    {
        var outcomes = _starvationService.CheckStarvation(_state.Value.TheParty);
        dispatcher.Dispatch(new TravelActions.StarvationChecked(outcomes));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.StarvationChecked action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new TravelActions.CheckTropicalStorm());
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task Handle(
        TravelActions.CheckTropicalStorm action,
        IDispatcher dispatcher)
    {
        var outcomes = _tropicalStormService.CheckExhaustion(_state.Value.TheParty, _state.Value.WeatherReport!);
        dispatcher.Dispatch(new TravelActions.TropicalStormChecked(outcomes));

        return Task.CompletedTask;
    }
}
