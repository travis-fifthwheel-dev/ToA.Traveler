using ToA.Traveler.Models;
using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.Services;

public class TropicalStormService(IDiceRoller diceRoller) : ITropicalStormService
{
    private readonly IDiceRoller _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));

    public IEnumerable<TropicalStormOutcome> CheckExhaustion(IEnumerable<Character> characters, WeatherReport weatherReport)
    {
        var outcomes = new List<TropicalStormOutcome>();

        if (weatherReport.IsTropicalStorm)
        {
            foreach (var character in characters)
            {
                var exhaustionGained = 1;

                if (_diceRoller.Roll(20) + character.ConstitutionSaveModifier < 10)
                    exhaustionGained += 1;

                outcomes.Add(new(character.Name, exhaustionGained));
            }
        }

        return outcomes;
    }
}

    public record TropicalStormOutcome(CharacterName CharacterName, int ExhaustionGained);
