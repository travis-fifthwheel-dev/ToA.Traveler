using ToA.Traveler.Models;
using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.Services;

public class HydrationService(IDiceRoller diceRoller) : IHydrationService
{
    private readonly IDiceRoller _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));

    public IEnumerable<HydrationOutcome> CheckHydration(IEnumerable<Character> theParty, WeatherReport weather, TravelPace pace)
    {
        var outcomes = new List<HydrationOutcome>();
        var baseDc = 15;
        var baseConSave = 15;

        foreach (var character in theParty)
        {
            var requiredConsumed = weather.Temperature is Temperature.ExtremeHeat ? 3M : 2M;
            var dcModifier = pace is TravelPace.Fast ? 5 : 0;
            var isHydrated = character.ConsumedWater >= requiredConsumed;
            var exhaustionGained = 0;

            if (!isHydrated)
            {
                var fortunes = new List<RollFortune>();

                if (character.Armor is ArmorType.Medium || character.Armor is ArmorType.Heavy)
                {
                    fortunes.Add(RollFortune.Disadvantage);

                    if (weather.Temperature is Temperature.ExtremeHeat)
                    {
                        var isConSaved = _diceRoller.Roll(20) + character.ConstitutionModifier >= baseConSave;

                        if (!isConSaved)
                            exhaustionGained += 1;
                    }
                }

                if (character.ConsumedWater > 0)
                    fortunes.Add(RollFortune.Advantage);

                var hydrationCheck = _diceRoller.RollWithFortunes(20, fortunes);

                var hydrationCheckResult = hydrationCheck >= baseDc + dcModifier;

                if (!hydrationCheckResult)
                    exhaustionGained += character.Exhaustion > 0 ? 2 : 1;
            }

            outcomes.Add(new(character.Name, isHydrated, exhaustionGained));
        }

        return outcomes;
    }
}

public record HydrationOutcome(CharacterName CharacterName, bool IsHydrated, int ExhaustionGained);