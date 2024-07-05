using ToA.Traveler.Models;
using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.Services;

public record WeatherReport(
    Temperature Temperature,
    Wind Wind,
    Precipitation Precipitation,
    bool IsTropicalStorm,
    List<string> TemperatureDisplay,
    List<string> WindDisplay,
    List<string> PrecipitationDisplay,
    List<string> TropicalStormDisplay);

public class WeatherTable(IDiceRoller diceRoller) : IWeatherTable
{
    private readonly IDiceRoller _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));

    public WeatherReport Roll()
    {
        var temperatureRoll = _diceRoller.Roll(20);
        var windRoll = _diceRoller.Roll(20);
        var precipitationRoll = _diceRoller.Roll(20);

        var precipitation = Precipitation.None;
        var temperature = Temperature.Normal;
        var wind = Wind.None;

        switch (precipitationRoll)
        {
            case <= 12:
                precipitation = Precipitation.None;
                break;
            case <= 17:
                precipitation = Precipitation.LightRain;
                break;
            case <= 20:
                precipitation = Precipitation.HeavyRain;
                break;
        }

        switch (temperatureRoll)
        {
            case <= 14:
                temperature = Temperature.Normal;
                break;
            case <= 17:
                temperature = Temperature.Colder;
                break;
            case <= 20:
                temperature = Temperature.ExtremeHeat;
                break;
        }

        var isTropicalStorm = precipitation is Precipitation.HeavyRain && _diceRoller.Roll(100) <= 25;

        if (isTropicalStorm)
            wind = Wind.Strong;
        else
            switch (windRoll)
            {
                case <= 12:
                    wind = Wind.None;
                    break;
                case <= 17:
                    wind = Wind.Light;
                    break;
                case <= 20:
                    wind = Wind.Strong;
                    break;
            }

        var tempDisplay = TemperatureDisplay(temperature);
        var windDisplay = WindDisplay(wind);
        var precipDisplay = PrecipitationDisplay(precipitation);
        var tropDisplay = TropicalStormDisplay(isTropicalStorm);

        return new(
            temperature,
            wind,
            precipitation,
            isTropicalStorm,
            tempDisplay.ToList(),
            windDisplay.ToList(),
            precipDisplay.ToList(),
            tropDisplay.ToList());
    }

    private IEnumerable<string> TemperatureDisplay(Temperature temperature)
    {
        var colder = 95 - (_diceRoller.Roll(4) * 10);

        switch (temperature)
        {
            case Temperature.Normal:
                yield return "Normal temperature, 95F.";
                break;
            case Temperature.Colder:
                yield return $"Colder than normal, {colder}F.";
                break;
            case Temperature.ExtremeHeat:
                yield return "Extreme heat.";
                yield return "If exposed to heat and without water, must succeed on a CON save each hour or gain a level of exhaustion.";
                yield return "DC is 5 after first hour, +1 for each additional hour.";
                yield return "Medium/Heavy armor imposes disadvantage on the save.";
                break;
        }
    }

    private IEnumerable<string> WindDisplay(Wind wind)
    {
        switch (wind)
        {
            case Wind.None:
                yield return "No wind.";
                break;
            case Wind.Light:
                yield return "Light wind.";
                break;
            case Wind.Strong:
                yield return "Strong wind.";
                yield return "Disadvantage on ranged attacks.";
                yield return "Disadvantage on Perception checks that rely on hearing.";
                yield return "Extinguishes open flames, disperses fog.";
                yield return "Flying creatures must land at the end of their turn or fall.";
                break;
        }
    }

    private IEnumerable<string> PrecipitationDisplay(Precipitation precipitation)
    {
        switch (precipitation)
        {
            case Precipitation.None:
                yield return "No rain.";
                break;
            case Precipitation.LightRain:
                yield return "Light rain.";
                break;
            case Precipitation.HeavyRain:
                yield return "Heavy rain.";
                yield return "Visibility limited to 50ft.";
                yield return "Missile weapon ranges halved.";
                break;
        }
    }

    private IEnumerable<string> TropicalStormDisplay(bool isTropicalStorm)
    {
        if (isTropicalStorm)
        {
            yield return "Tropical storm.";
            yield return "Travel by river is impossible, rivers flood after 15 mins.";
            yield return "For each day travelling on foot, characters gain a level of exhaustion + 1 more if they fail a DC 10 CON save.";
            yield return "Skill checks to avoid becoming lost are made with disadvantage.";
            yield return "Foraging is impossible.";
            yield return "Using rain catchers is impossible.";
        }
    }
}
