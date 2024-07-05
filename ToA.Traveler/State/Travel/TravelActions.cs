using ToA.Traveler.Models;
using ToA.Traveler.Services;

namespace ToA.Traveler.State.Travel;

public static class TravelActions
{
    public record DayStarted();
    public record ReportWeather();
    public record WeatherReported(WeatherReport WeatherReport);
    public record StartTravel();
    public record TravelStarted();
    public record ConfirmTravelJobs();
    public record TravelJobsConfirmed(IEnumerable<Character> TheParty);
    public record ConfirmTravelSpecs();
    public record TravelSpecsConfirmed(TravelDirection TravelDirection, TravelPace TravelPace, TerrainType TravelTerrain);
    public record StartNavigation(WeatherReport Weather, Character Navigator, IEnumerable<Character> Cartographers, TravelPace Pace, TerrainType Terrain, TravelDirection Direction, bool UsingCanoe);
    public record NavigationStarted(NavigationOutcome Outcome);
    public record RollEncounters(IEnumerable<Character> Foragers, IEnumerable<Character> Scouts, IEnumerable<Character> Cartographers, Character Navigator, TravelPace Pace, TerrainType Terrain);
    public record EncountersRolled(IEnumerable<EncounterOutcome> Encounters);
    public record StartForaging(IEnumerable<Character> Foragers, TravelPace Pace, TerrainType Terrain, WeatherReport Weather);
    public record ForagingStarted(IEnumerable<ForageOutcome> Outcomes);
    public record EndNavigation();
    public record CheckHydration();
    public record HydrationChecked(IEnumerable<HydrationOutcome> Outcomes);
    public record CheckStarvation();
    public record StarvationChecked(IEnumerable<StarvationOutcome> Outcomes);
    public record CheckTropicalStorm();
    public record TropicalStormChecked(IEnumerable<TropicalStormOutcome> Outcomes);
    public record ConsumeFood(CharacterName CharacterName, int Pounds = 1);
    public record ConsumeWater(CharacterName CharacterName, decimal Gallons = 0.5M);
    public record MakeCamp();
    public record SwapArmor(CharacterName CharacterName, ArmorType ArmorType);
}
