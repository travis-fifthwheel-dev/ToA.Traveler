using ToA.Traveler.Models;

namespace ToA.Traveler.Services;

public class StarvationService : IStarvationService
{
    public IEnumerable<StarvationOutcome> CheckStarvation(IEnumerable<Character> characters)
    {
        var outcomes = new List<StarvationOutcome>();

        foreach (var character in characters)
        {
            var exhaustionGained = 0;

            if (character.DaysWithoutFood > character.ConstitutionModifier + 3)
                exhaustionGained = 1;

            outcomes.Add(new(character.Name, exhaustionGained));
        }

        return outcomes;
    }
}

public record StarvationOutcome(CharacterName CharacterName, int ExhaustionGained);
