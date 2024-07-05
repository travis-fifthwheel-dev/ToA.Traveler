using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToA.Traveler.Models;
using ToA.Traveler.Services;
using ToA.Traveler.Services.Contracts;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.NavigatorComponent;

public class NavigatorBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    protected WeatherReport? WeatherReport => State.Value.WeatherReport;
    protected Character? Navigator => State.Value.Navigator;

    protected bool IsForcedSlowPace => ForcedSlowPaceCount > ForcedSlowPaceThreshold;
    protected IEnumerable<Character> TheParty => State.Value.TheParty;
    protected IEnumerable<Character> Cartographers => State.Value.Cartographers;
    protected IEnumerable<Character> Foragers => State.Value.Foragers;

    protected TravelPace Pace = TravelPace.Normal;
    protected TerrainType Terrain = TerrainType.Jungle;

    private int ForcedSlowPaceCount => TheParty.Count(x => x.Exhaustion >= 2);
    private int ForcedSlowPaceThreshold => (int)Math.Floor((double)TheParty.Count() / (double)2.0);

    protected List<TravelerDirectionDropItem> DropItems { get; set; } =
    [
        new TravelerDirectionDropItem { Icon = Icons.Material.TwoTone.Hiking, Direction = TravelDirection.North, Identifier = "11" },
    ];

    protected override void OnInitialized()
    {
        Pace = IsForcedSlowPace ? TravelPace.Slow : TravelPace.Normal;

        base.OnInitialized();
    }

    protected void ItemUpdated(MudItemDropInfo<TravelerDirectionDropItem> dropItem)
    {
        dropItem.Item.Identifier = dropItem.DropzoneIdentifier;
        dropItem.Item.Direction = dropItem.DropzoneIdentifier switch
        {
            "00" => TravelDirection.NorthWest,
            "01" => TravelDirection.North,
            "02" => TravelDirection.NorthEast,
            "20" => TravelDirection.SouthWest,
            "21" => TravelDirection.South,
            "22" => TravelDirection.SouthEast,
            _ => throw new ArgumentOutOfRangeException(nameof(dropItem.DropzoneIdentifier))
        };
    }

    protected void StartNavigation()
    {
        if (DropItems.Single().Identifier == "11")
            return;

        if (Navigator is null)
            throw new Exception("Missing party navigator.");

        if (WeatherReport is null)
            throw new Exception("Missing weather report.");

        var direction = DropItems.Single().Direction;

        Dispatcher.Dispatch(new TravelActions.TravelSpecsConfirmed(direction, Pace, Terrain));
    }
}

public class TravelerDirectionDropItem
{
    public string Icon { get; set; }
    public TravelDirection Direction { get; set; }
    public string Identifier { get; set; }
}
