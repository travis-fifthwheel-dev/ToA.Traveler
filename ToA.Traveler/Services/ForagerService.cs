using System.Diagnostics;
using ToA.Traveler.Models;
using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.Services;

public class ForagerService(
    IDiceRoller diceRoller) : IForagerService
{
    private readonly IDiceRoller _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));

    public IEnumerable<ForageOutcome> Forage(IEnumerable<Character> foragers, TravelPace pace, TerrainType terrain, WeatherReport weather)
    {
        var stackTrace = new StackTrace();
        var stackFrame = stackTrace.GetFrame(1); // Get the caller frame (1 level up in the call stack)
        var methodBase = stackFrame.GetMethod();

        Console.WriteLine($"Called from: {methodBase.DeclaringType.FullName}.{methodBase.Name}");

        var baseDc = 10;
        var modifiedDc = baseDc + GetWeatherModifier(weather);

        foreach (var forager in foragers)
        {
            var fortune = new List<RollFortune>();

            if (forager.Exhaustion > 0)
                fortune.Add(RollFortune.Disadvantage);

            if (forager.IsOutlander)
                fortune.Add(RollFortune.Advantage);

            if (pace is TravelPace.Fast)
                yield return new ForageOutcome(forager.Name, false); // Foraging is not possible at fast pace

            var survivalModifier = forager.IsSurvivalProficient ? forager.SurvivalModifier : 0;
            var survivalRoll = _diceRoller.RollWithFortunes(20, fortune);
            var isSuccess = survivalRoll + survivalModifier >= modifiedDc;
            var poundsOfFood = isSuccess ? GetPoundsOfFood(forager) : 0;
            var gallonsOfWater = isSuccess ? GetGallonsOfWater(forager) : 0;
            var specialPlant = isSuccess ? GetSpecialPlant(terrain) : null;
            yield return new ForageOutcome(forager.Name, isSuccess, poundsOfFood, gallonsOfWater, specialPlant);
        }
    }

    private int GetGallonsOfWater(Character forager)
    {
        var modifier = forager.IsSurvivalProficient ? forager.SurvivalModifier : 0;

        if (forager.IsOutlander)
            return _diceRoller.Roll(6) + modifier;

        return _diceRoller.Roll(4) + modifier;
    }

    private int GetPoundsOfFood(Character forager)
    {
        if (forager.IsOutlander)
            return _diceRoller.Roll(6) + (forager.IsSurvivalProficient ? forager.SurvivalModifier : forager.WisdomModifier);

        return _diceRoller.Roll(4) + forager.WisdomModifier;
    }

    private Plant GetSpecialPlant(TerrainType terrain)
    {
        var plantTable = Plants.FoundInTerrain(terrain);
        return _diceRoller.RollTable(plantTable);
    }

    private int GetWeatherModifier(WeatherReport weather) =>
        GetWindModifer(weather.Wind) + GetPrecipitationModifier(weather.Precipitation);

    private int GetWindModifer(Wind wind) =>
        wind switch
        {
            Wind.None => 0,
            Wind.Light => 0,
            Wind.Strong => 4,
            _ => 0,
        };

    private int GetPrecipitationModifier(Precipitation precipitation) =>
        precipitation switch
        {
            Precipitation.None => 0,
            Precipitation.LightRain => 2,
            Precipitation.HeavyRain => 8,
            _ => 0,
        };
}

public record ForageOutcome(CharacterName ForagerName, bool IsSuccess, int PoundsOfFood = 0, int GallonsOfWater = 0, Plant? SpecialPlant = null);