using ToA.Traveler.Models;

namespace ToA.Traveler.Services.Contracts;
public interface IEncounterService
{
    IEnumerable<EncounterOutcome> Encounter(
        IEnumerable<Character> foragers,
        IEnumerable<Character> scouts,
        IEnumerable<Character> cartographers,
        Character navigator,
        TravelPace pace,
        TerrainType terrain);
}