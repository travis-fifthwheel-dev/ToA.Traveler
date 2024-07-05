using ToA.Traveler.Models;

namespace ToA.Traveler.Services;
public interface IStarvationService
{
    IEnumerable<StarvationOutcome> CheckStarvation(IEnumerable<Character> characters);
}