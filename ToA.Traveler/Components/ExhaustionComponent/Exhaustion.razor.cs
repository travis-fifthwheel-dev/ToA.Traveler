using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.Models;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.ExhaustionComponent;

public class ExhaustionBase : FluxorComponent
{
    [Inject]
    public IState<TravelState> State { get; set; } = default!;

    public IEnumerable<Character> TheParty => State.Value.TheParty;
}
