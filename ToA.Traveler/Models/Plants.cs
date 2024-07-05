namespace ToA.Traveler.Models;

public class Plant
{
    public string Name { get; set; } = string.Empty;       
}

public static class Plants
{
    public static IEnumerable<Plant> FoundInTerrain(TerrainType terrain) =>
        terrain switch
        {
            TerrainType.Jungle => [
                Baneberry,
                BlueAgaric,
                ChultanLavender,
                Deathbane,
                GavaFruit,
                GiantHorsetail,
                GiantThornapple,
                GlueBlossom,
                Heartflower,
                JakaRoot,
                Pitmoss,
                RainRocks,
                RazorFern,
                RedBlisterbush,
                TebarraVine,
                Spellflower,
                SpottedFlatbean,
                StickyVine,
                WeepingTrumpet,
                WhiteBellflower],
            TerrainType.River => [
                ChultanMint,
                ChultanTuber,
                GiantBladderwort],
            TerrainType.Coast => [
                ChultanMint,
                ChultanTuber,
                GiantBladderwort],
            TerrainType.Lake => [
                ChultanMint,
                ChultanTuber,
                GiantBladderwort],
            TerrainType.Mountain => [
                Bloodflower,
                GiantNepenthes],
            TerrainType.Swamp => [
                ChultanMint,
                ChultanTuber,
                GiantBladderwort],
            TerrainType.Wasteland => [
                Bloodflower,
                GiantNepenthes],
            _ => []
        };

    public static Plant Baneberry => new()
    {
        Name = "Baneberry",
    };

    public static Plant Bloodflower => new()
    {
        Name = "Bloodflower",
    };

    public static Plant BlueAgaric => new()
    {
        Name = "Blue Agaric",
    };

    public static Plant ChultanLavender => new()
    {
        Name = "Chultan Lavender",
    };

    public static Plant ChultanMint => new()
    {
        Name = "Chultan Mint",
    };

    public static Plant ChultanTuber => new()
    {
        Name = "Chultan Tuber",
    };

    public static Plant Deathbane => new()
    {
        Name = "Deathbane",
    };

    public static Plant GavaFruit => new()
    {
        Name = "Gava Fruit",
    };

    public static Plant GiantBladderwort => new()
    {
        Name = "Giant Bladderwort",
    };

    public static Plant GiantHorsetail => new()
    {
        Name = "Giant Horsetail",
    };

    public static Plant GiantNepenthes => new()
    {
        Name = "Giant Nepenthes",
    };
    public static Plant GiantThornapple => new()
    {
        Name = "Giant Thornapple",
    };

    public static Plant GlueBlossom => new()
    {
        Name = "Glue Blossom",
    };

    public static Plant Heartflower => new()
    {
        Name = "Heartflower",
    };

    public static Plant JakaRoot => new()
    {
        Name = "Jaka Root",
    };

    public static Plant Pitmoss => new()
    {
        Name = "Pitmoss",
    };

    public static Plant RainRocks => new()
    {
        Name = "Rain Rocks",
    };

    public static Plant RazorFern => new()
    {
        Name = "Razor Fern",
    };
    public static Plant RedBlisterbush => new()
    {
        Name = "Red Blisterbush",
    };

    public static Plant TebarraVine => new()
    {
        Name = "Tebarra Vine",
    };

    public static Plant Spellflower => new()
    {
        Name = "Spellflower",
    };

    public static Plant SpottedFlatbean => new()
    {
        Name = "Spotted Flatbean",
    };

    public static Plant StickyVine => new()
    {
        Name = "Sticky Vine",
    };

    public static Plant WeepingTrumpet => new()
    {
        Name = "Weeping Trumpet",
    };

    public static Plant WhiteBellflower => new()
    {
        Name = "White Bellflower",
    };
}