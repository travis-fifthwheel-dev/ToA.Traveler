using ToA.Traveler.Models;

namespace ToA.Traveler.Services.Contracts;
public interface IHydrationService
{
    IEnumerable<HydrationOutcome> CheckHydration(IEnumerable<Character> theParty, WeatherReport weather, TravelPace pace);
}