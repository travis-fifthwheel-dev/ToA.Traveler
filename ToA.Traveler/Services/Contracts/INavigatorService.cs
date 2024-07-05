using ToA.Traveler.Models;

namespace ToA.Traveler.Services.Contracts;

public interface INavigatorService
{
    public NavigationOutcome Navigate(
        WeatherReport weather, 
        Character navigator, 
        IEnumerable<Character> Cartographers, 
        TravelPace pace, 
        TerrainType terrain, 
        TravelDirection intendedDirection, 
        bool usingCanoe);
}
