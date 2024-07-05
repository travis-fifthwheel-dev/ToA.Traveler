using ToA.Traveler.Models;

namespace ToA.Traveler.Services.Contracts;
public interface IForagerService
{
    IEnumerable<ForageOutcome> Forage(IEnumerable<Character> forager, TravelPace pace, TerrainType terrain, WeatherReport weather);
}