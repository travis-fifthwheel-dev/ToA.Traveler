namespace ToA.Traveler.Models;

public static class Characters
{
    public static IEnumerable<Character> TheParty { get; private set; }

    static Characters()
    {
        TheParty = [Godrick!, Teel!, Zephyr!, Valthrun!, Dravion!, Undril!, Hackinstone!];
    }

    public static Character Godrick = new()
    {
        Name = CharacterName.Godrick,
        SurvivalModifier = 0,
        PerceptionModifier = 0,
        ConstitutionModifier = 3,
        ConstitutionSaveModifier = 3,
        IsSurvivalProficient = false,
        IsCartographerProficient = false,
        IsOutlander = false,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.Heavy,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };

    public static Character Teel = new()
    {
        Name = CharacterName.Teel,
        SurvivalModifier = 3,
        PerceptionModifier = 3,
        ConstitutionModifier = 2,
        ConstitutionSaveModifier = 2,
        IsSurvivalProficient = true,
        IsCartographerProficient = false,
        IsOutlander = true,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.Light,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };

    public static Character Zephyr = new()
    {
        Name = CharacterName.Zephyr,
        SurvivalModifier = 2,
        PerceptionModifier = 2,
        ConstitutionModifier = 2,
        ConstitutionSaveModifier = 5,
        IsSurvivalProficient = false,
        IsCartographerProficient = false,
        IsOutlander = false,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.None,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };

    public static Character Valthrun = new()
    {
        Name = CharacterName.Valthrun,
        SurvivalModifier = 4,
        PerceptionModifier = 4,
        ConstitutionModifier = 1,
        ConstitutionSaveModifier = 2,
        IsSurvivalProficient = false,
        IsCartographerProficient = false,
        IsOutlander = false,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.Heavy,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };

    public static Character Dravion = new()
    {
        Name = CharacterName.Dravion,
        SurvivalModifier = 1,
        PerceptionModifier = 1,
        ConstitutionModifier = 2,
        ConstitutionSaveModifier = 2,
        IsSurvivalProficient = false,
        IsCartographerProficient = false,
        IsOutlander = false,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.Light,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };

    public static Character Undril = new()
    {
        Name = CharacterName.Undril,
        SurvivalModifier = 0,
        PerceptionModifier = 0,
        ConstitutionModifier = 1,
        ConstitutionSaveModifier = 0,
        IsSurvivalProficient = false,
        IsCartographerProficient = false,
        IsOutlander = false,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.Medium,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };

    public static Character Hackinstone = new()
    {
        Name = CharacterName.Hackinstone,
        SurvivalModifier = 4,
        PerceptionModifier = 0,
        ConstitutionModifier = 1,
        ConstitutionSaveModifier = 0,
        IsSurvivalProficient = true,
        IsCartographerProficient = true,
        IsOutlander = false,
        ConsumedRations = 0,
        DaysWithoutFood = 0,
        ConsumedWater = 0,
        Armor = ArmorType.Medium,
        Job = TravelJob.Scout,
        JobSelector = "Scout",
        Exhaustion = 0,
    };
}

public enum CharacterName
{
    Godrick,
    Teel,
    Zephyr,
    Valthrun,
    Dravion,
    Undril,
    Hackinstone,
}

public enum ArmorType
{
    None,
    Light,
    Medium,
    Heavy,
}

public enum TravelJob
{
    Navigator, // Must have a navigator to travel - makes daily navigation check
    Cartographer, // Maps the terrain - the party does not record their progress on the map without a cartographer
    Forager, // Breaks off short distances from the party to gather food and water - cannot forage at a fast pace.
             // Separated characters take 1d4+1 rounds to return to the party at start of an encounter
    Scout, // Default job - only job that contributes passive perception to noticing threats party may encounter
}

public class Character
{
    public CharacterName Name;
    public int SurvivalModifier = 0;
    public int PerceptionModifier = 10;
    public int ConstitutionModifier = 0;
    public int ConstitutionSaveModifier = 0;
    public int WisdomModifier = 0;
    public bool IsSurvivalProficient = false;
    public bool IsCartographerProficient = false;
    public bool IsOutlander = false;
    public int ConsumedRations = 0;
    public int DaysWithoutFood = 0;
    public decimal ConsumedWater = 0.0M;
    public bool HasConsumedTaintedWater = false;
    public ArmorType Armor = ArmorType.None;
    public TravelJob Job { get; set; } = TravelJob.Scout;
    public string JobSelector { get; set; } = "Scout";
    public int Exhaustion { get; set; } = 0;

    public void Hydrate(decimal waterConsumed, bool isWaterClean)
    {
        ConsumedWater += waterConsumed;
        HasConsumedTaintedWater = waterConsumed > 0 && !isWaterClean;
    }

    public void Eat(int rationsConsumed)
    {
        ConsumedRations += rationsConsumed;
    }
}