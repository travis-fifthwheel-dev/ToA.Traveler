using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Models;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Pages;

public class PartyBase : FluxorComponent
{
    [Inject]
    public IState<TravelState> State { get; set; } = default!;

    protected List<Character> TheParty => State.Value.TheParty.OrderBy(x => x.Name).ToList();
}
