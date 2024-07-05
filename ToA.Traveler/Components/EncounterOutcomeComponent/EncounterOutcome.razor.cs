using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using ToA.Traveler.State.Travel;

namespace ToA.Traveler.Components.EncounterOutcomeComponent;

public class EncounterOutcomeBase : FluxorComponent
{
    [Inject]
    protected IState<TravelState> State { get; set; } = default!;

    protected IEnumerable<Services.EncounterOutcome>? EncounterOutcomes => State.Value.EncounterOutcomes;
}