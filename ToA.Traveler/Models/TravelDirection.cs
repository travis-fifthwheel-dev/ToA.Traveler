namespace ToA.Traveler.Models;

public enum TravelDirection
{
    North,
    NorthEast,
    SouthEast,
    South,
    SouthWest,
    NorthWest,
}

public static class TargetDirectionExtensions
{
    public static TravelDirection AsDirection(this int direction)
    {
        return direction switch
        {
            1 => TravelDirection.North,
            2 => TravelDirection.NorthEast,
            3 => TravelDirection.SouthEast,
            4 => TravelDirection.South,
            5 => TravelDirection.SouthWest,
            6 => TravelDirection.NorthWest,
            _ => throw new ArgumentOutOfRangeException(nameof(direction)),
        };
    }

    public static string AsDisplay(this TravelDirection direction)
    {
        return direction switch
        {
            TravelDirection.North => "North",
            TravelDirection.NorthEast => "North East",
            TravelDirection.SouthEast => "South East",
            TravelDirection.South => "South",
            TravelDirection.SouthWest => "South West",
            TravelDirection.NorthWest => "North West",
            _ => throw new ArgumentOutOfRangeException(nameof(direction)),
        };
    }
}