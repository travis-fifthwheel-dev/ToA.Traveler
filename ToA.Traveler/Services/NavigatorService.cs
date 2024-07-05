using ToA.Traveler.Models;
using ToA.Traveler.Services.Contracts;
namespace ToA.Traveler.Services;

public class NavigatorService(IDiceRoller diceRoller) : INavigatorService
{
    private readonly IDiceRoller _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));

    public NavigationOutcome Navigate(
        WeatherReport weather, 
        Character navigator, 
        IEnumerable<Character> cartographers, 
        TravelPace pace, 
        TerrainType terrain, 
        TravelDirection intendedDirection, 
        bool usingCanoe)
    {
        var travelPaceMod = GetTravelPaceModifier(pace);
        var terrainMod = GetTerrainModifier(terrain);
        var weatherMod = GetWeatherModifier(weather);
        var cartographerMod = GetCartographerMod(cartographers);
        var survivalDc = travelPaceMod + terrainMod + weatherMod + cartographerMod;

        var survivalRoll = GetSurvivalRoll(weather, navigator);
        var survivalRollWithMod = survivalRoll + navigator.SurvivalModifier;

        var isPartyLost = survivalRollWithMod < survivalDc;

        var travelDirection = GetDirection(intendedDirection, isPartyLost);
        var hexCount = GetHexCount(terrain, pace, usingCanoe);

        return new NavigationOutcome(isPartyLost, cartographers.Any(), navigator.IsOutlander, travelDirection, hexCount);
    }

    private int GetSurvivalRoll(WeatherReport weather, Character navigator)
    {
        var fortunes = new List<RollFortune>();

        if(navigator.IsSurvivalProficient)
            fortunes.Add(RollFortune.Advantage);

        if(weather.Precipitation is Precipitation.HeavyRain)
            fortunes.Add(RollFortune.Disadvantage);

        if (weather.IsTropicalStorm)
            fortunes.Add(RollFortune.Disadvantage);

        if (navigator.Exhaustion > 0)
            fortunes.Add(RollFortune.Disadvantage);

        var rollFortune = _diceRoller.GetRollType(fortunes);

        return _diceRoller.RollWithFortunes(20, fortunes);
    }

    private int GetHexCount(TerrainType terrain, TravelPace pace, bool usingCanoe)
    {
        var baseHexCount = (terrain, usingCanoe) switch
        {
            (TerrainType.River, true) => 2,
            (TerrainType.Lake, true) => 2,
            _ => 1,
        };

        if (pace is TravelPace.Normal)
            return baseHexCount;

        var paceModCheck = _diceRoller.Roll(4);

        var paceModifier = (pace, paceModCheck) switch
        {
            (TravelPace.Slow, <= 2) => -1,
            (TravelPace.Fast, >= 3) => 1,
            _ => 0,
        };

        return baseHexCount + paceModifier;
    }
    private int GetCartographerMod(IEnumerable<Character> cartographers)
    {
        if (cartographers.Any(x => x.IsCartographerProficient))
            return 2;

        return 0;
    }
    private int GetWeatherModifier(WeatherReport weather)
    {
        return weather.Precipitation switch
        {
            Precipitation.HeavyRain => 5,
            _ => 0,
        };
    }


    private int GetTerrainModifier(TerrainType terrain) =>
        terrain switch
        {
            TerrainType.Coast => 10,
            TerrainType.Lake => 10,
            _ => 15,
        };

    private int GetTravelPaceModifier(TravelPace pace) =>
        pace switch
        {
            TravelPace.Slow => 5,
            TravelPace.Fast => -5,
            _ => 0,
        };

    public TravelDirection GetDirection(TravelDirection intendedDirection, bool isPartyLost)
    {
        if(!isPartyLost)
            return intendedDirection;

        var directionRoll = _diceRoller.Roll(6);

        return directionRoll.AsDirection();
    }
}

public record NavigationOutcome(bool IsPartyLost, bool UsedCartographers, bool IsNavigatorOutlander, TravelDirection TravelDirection, int HexCount);