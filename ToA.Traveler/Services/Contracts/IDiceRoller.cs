namespace ToA.Traveler.Services.Contracts;

public interface IDiceRoller
{
    public int Roll(int sides, int count = 1);
    public int RollWithAdvantage(int sides);
    public int RollWithDisadvantage(int sides);
    public int RollWithFortunes(int sides, IEnumerable<RollFortune> Fortunes);
    public RollFortune GetRollType(IEnumerable<RollFortune> RollTypes);
    public T RollTable<T>(IEnumerable<T> table);
}

public enum RollFortune
{
    Normal,
    Advantage,
    Disadvantage,
}
