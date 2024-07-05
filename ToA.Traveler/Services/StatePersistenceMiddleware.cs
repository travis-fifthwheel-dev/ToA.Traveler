using Fluxor;
using System.Text.Json;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Services;

public class StatePersistenceMiddleware : Middleware
{
    private readonly ILogger<StatePersistenceMiddleware> _logger;
    private readonly IState<TravelState> _state;
    private readonly StatePersistenceService _statePersistenceService;

    public StatePersistenceMiddleware(ILogger<StatePersistenceMiddleware> logger, IState<TravelState> state, StatePersistenceService statePersistenceService)
    {
        _logger = logger;
        _state = state;
        _statePersistenceService = statePersistenceService;
    }

    public override async Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        var loadedState = await _statePersistenceService.LoadStateAsync();
        var featureName = typeof(TravelState).FullName;
        var feature = store.Features[featureName];
        feature?.RestoreState(loadedState);
        _logger.LogInformation("State loaded successfully from local storage.");
        await base.InitializeAsync(dispatcher, store);
    }

    public override async void AfterDispatch(object action)
    {
        try
        {
            await _statePersistenceService.SaveStateAsync(_state.Value);
            _logger.LogInformation("State saved to local storage.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving the state: {ex.Message}");
        }
    }
}
