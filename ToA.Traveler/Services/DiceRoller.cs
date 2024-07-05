using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.Services;

public class DiceRoller : IDiceRoller
{
    public int Roll(int sides, int count = 1)
    {
        var result = 0;
        var random = new Random();

        for (var i = 0; i < count; i++)
            result += random.Next(1, sides + 1);

        return result;
    }

    public int RollWithAdvantage(int sides)
    {
        var firstRoll = Roll(sides);
        var secondRoll = Roll(sides);

        return Math.Max(firstRoll, secondRoll);
    }

    public int RollWithDisadvantage(int sides)
    {
        var firstRoll = Roll(sides);
        var secondRoll = Roll(sides);

        return Math.Min(firstRoll, secondRoll);
    }

    public int RollWithFortunes(int sides, IEnumerable<RollFortune> fortunes)
    {
        var rollType = GetRollType(fortunes);

        if(rollType is RollFortune.Advantage)
            return RollWithAdvantage(sides);

        if (rollType is RollFortune.Disadvantage)
            return RollWithDisadvantage(sides);

        return Roll(sides);
    }

    public RollFortune GetRollType(IEnumerable<RollFortune> rollTypes)
    {
        if(rollTypes.Any(x => x is RollFortune.Advantage) && rollTypes.Any(x => x is RollFortune.Disadvantage))
            return RollFortune.Normal;

        if(rollTypes.Any(x => x is RollFortune.Advantage))
            return RollFortune.Advantage;

        if(rollTypes.Any(x => x is RollFortune.Disadvantage))
            return RollFortune.Disadvantage;

        return RollFortune.Normal;
    }

    public T RollTable<T>(IEnumerable<T> table)
    {
        var roll = Roll(table.Count());
        return table.ElementAt(roll - 1);
    }
}