using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToA.Traveler.Models;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.TravelJobsComponent;

public class TravelJobsBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    protected bool NavigatorAssigned = false;
    protected bool PartyHasNavigator => State.Value.Navigator is not null;

    protected override void OnInitialized()
    {
        NavigatorAssigned = PartyHasNavigator;

        base.OnInitialized();
    }

    public void TravelJobsConfirmed()
    {
        if(!NavigatorAssigned)
            return;

        Dispatcher.Dispatch(new TravelActions.TravelJobsConfirmed(TheParty));
    }
    protected void ItemUpdated(MudItemDropInfo<Character> dropItem)
    {
        dropItem.Item.Job = (TravelJob)Enum.Parse(typeof(TravelJob), dropItem.DropzoneIdentifier);
        dropItem.Item.JobSelector = dropItem.DropzoneIdentifier;

        NavigatorAssigned = PartyHasNavigator || dropItem.Item.Job is TravelJob.Navigator;
    }

    protected List<Character> TheParty =
    [
        Characters.Godrick,
        Characters.Teel,
        Characters.Zephyr,
        Characters.Valthrun,
        Characters.Dravion,
        Characters.Undril,
        Characters.Hackinstone,
    ];
}
