using Fluxor;
using ToA.Traveler.Models;
using ToA.Traveler.Services;

namespace ToA.Traveler.State.Travel;

[FeatureState]
public record TravelState
{
    public TravelState() { }

    public int Day { get; init; } = 0;
    public bool IsCamping { get; init; } = false;
    public bool IsNavigating { get; init; } = false;
    public bool IsDayEnded { get; init; } = true;
    public bool IsTraveling { get; init; } = false;
    public bool IsConfirmingTravelJobs { get; init; } = false;
    public bool IsConfirmingTravelSpecs { get; init; } = false;
    public NavigationOutcome PreviousNavigationOutcome { get; init; }
    public WeatherReport? WeatherReport { get; init; }
    public NavigationOutcome? NavigationOutcome { get; init; }
    public List<EncounterOutcome>? EncounterOutcomes { get; init; }
    public List<ForageOutcome>? ForageOutcomes { get; init; }
    public List<HydrationOutcome>? HydrationOutcomes { get; init; }
    public List<StarvationOutcome>? StarvationOutcomes { get; init; }
    public List<TropicalStormOutcome>? TropicalStormOutcomes { get; init; }
    public List<Character> TheParty { get; init; } = Characters.TheParty.ToList();
    public Character? Navigator => TheParty?.SingleOrDefault(x => x.Job is TravelJob.Navigator);
    public List<Character> Cartographers => TheParty?.Where(x => x.Job is TravelJob.Cartographer).ToList() ?? [];
    public List<Character> Foragers => TheParty?.Where(x => x.Job is TravelJob.Forager).ToList() ?? [];
    public List<Character> Scouts => TheParty?.Where(x => x.Job is TravelJob.Scout).ToList() ?? [];
    public TravelPace TravelPace { get; init; }
    public TravelDirection TravelDirection { get; init; }
    public TerrainType TravelTerrain { get; init; }
}
