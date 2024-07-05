using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Models;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.CharacterCardComponent;

public class CharacterCardBase : FluxorComponent
{
    [Parameter]
    public Character Character { get; set; } = default!;

    [Inject]
    protected IDispatcher Dispatcher { get; set; } = default!;

    protected void SwapArmor(ArmorType armorType)
    {
        Dispatcher.Dispatch(new TravelActions.SwapArmor(Character.Name, armorType));
    }
}
