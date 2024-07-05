using Blazored.LocalStorage;
using System.Text.Json;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Services;

public class StatePersistenceService
{
    private const string StateKey = "TravelState";
    private readonly ILocalStorageService _localStorageService;

    public StatePersistenceService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task SaveStateAsync(TravelState state)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(state, options);
        await _localStorageService.SetItemAsync(StateKey, jsonString);
    }

    public async Task<TravelState> LoadStateAsync()
    {
        var jsonString = await _localStorageService.GetItemAsync<string>(StateKey);
        var state = jsonString != null
            ? JsonSerializer.Deserialize<TravelState>(jsonString) ?? new TravelState()
            : new TravelState();
        return state;
    }

    public async Task ClearStateAsync()
    {
        await _localStorageService.RemoveItemAsync(StateKey);
    }
}
