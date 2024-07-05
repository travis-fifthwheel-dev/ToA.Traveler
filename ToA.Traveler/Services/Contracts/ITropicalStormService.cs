using ToA.Traveler.Models;

namespace ToA.Traveler.Services;
public interface ITropicalStormService
{
    IEnumerable<TropicalStormOutcome> CheckExhaustion(IEnumerable<Character> characters, WeatherReport weatherReport);
}