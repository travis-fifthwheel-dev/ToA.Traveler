using ToA.Traveler.Models;
using ToA.Traveler.Services.Contracts;

namespace ToA.Traveler.Services;

public class EncounterService(IDiceRoller diceRoller) : IEncounterService
{
    private readonly IDiceRoller _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));

    public IEnumerable<EncounterOutcome> Encounter(
        IEnumerable<Character> foragers, 
        IEnumerable<Character> scouts, 
        IEnumerable<Character> cartographers, 
        Character navigator, 
        TravelPace pace, 
        TerrainType terrain)
    {
        var baseDc = 16;
        var dawnEncounterRoll = _diceRoller.Roll(20);
        var dawnEncounterStealth = _diceRoller.Roll(20);
        var dayEncounterRoll = _diceRoller.Roll(20);
        var dayEncounterStealth = _diceRoller.Roll(20);
        var duskEncounterRoll = _diceRoller.Roll(20);
        var duskEncounterStealth = _diceRoller.Roll(20);

        var paceModifier = pace is TravelPace.Fast ? -5 : 0;

        var encounters = new List<EncounterOutcome>();

        if (dawnEncounterRoll >= baseDc)
        {
            var partySize = foragers.Count() + scouts.Count() + cartographers.Count() + 1;
            var isPartyTargeted = _diceRoller.Roll(partySize) > partySize - foragers.Count();
            var partyDelay = _diceRoller.Roll(4) + 1;
            var targetedForager = foragers.ElementAt(_diceRoller.Roll(foragers.Count() - 1));
            var encounterDelays = isPartyTargeted ?
                foragers.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3))).ToList() :
                    [..cartographers.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3))),
                     new EncounterDelay(CharacterName: navigator.Name, Delay: _diceRoller.Roll(3)),
                     ..scouts.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3)))];

            encounters.Add(new EncounterOutcome(
                IsEncounter: true,
                Terrain: terrain,
                EncounterTableRoll: _diceRoller.Roll(100),
                CanHide: pace is TravelPace.Slow,
                IsPartyTargeted: isPartyTargeted,
                TargetedForager: targetedForager.Name,
                ThreatSpotted: scouts.Any(x => _diceRoller.Roll(20) + x.PerceptionModifier + paceModifier >= dawnEncounterStealth),
                EncounterDelay: encounterDelays));
        }
        else
            encounters.Add(new EncounterOutcome(IsEncounter: false, Terrain: terrain));

        if (dayEncounterRoll >= baseDc)
        {
            var partySize = foragers.Count() + scouts.Count() + cartographers.Count() + 1;
            var isPartyTargeted = _diceRoller.Roll(partySize) > partySize - foragers.Count();
            var partyDelay = _diceRoller.Roll(4) + 1;
            var targetedForager = foragers.ElementAt(_diceRoller.Roll(foragers.Count() - 1));
            var encounterDelays = isPartyTargeted ?
                foragers.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3))).ToList() :
                    [..cartographers.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3))),
                     new EncounterDelay(CharacterName: navigator.Name, Delay: _diceRoller.Roll(3)),
                     ..scouts.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3)))];

            encounters.Add(new EncounterOutcome(
                IsEncounter: true,
                Terrain: terrain,
                EncounterTableRoll: _diceRoller.Roll(100),
                CanHide: pace is TravelPace.Slow,
                IsPartyTargeted: isPartyTargeted,
                TargetedForager: targetedForager.Name,
                ThreatSpotted: scouts.Any(x => _diceRoller.Roll(20) + x.PerceptionModifier + paceModifier >= dayEncounterStealth),
                EncounterDelay: encounterDelays));
        }
        else
            encounters.Add(new EncounterOutcome(IsEncounter: false, Terrain: terrain));

        if (duskEncounterRoll >= baseDc)
        {
            var partySize = foragers.Count() + scouts.Count() + cartographers.Count() + 1;
            var isPartyTargeted = _diceRoller.Roll(partySize) > partySize - foragers.Count();
            var partyDelay = _diceRoller.Roll(4) + 1;
            var targetedForager = foragers.ElementAt(_diceRoller.Roll(foragers.Count() - 1));
            var encounterDelays = isPartyTargeted ?
                foragers.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3))).ToList() :
                    [..cartographers.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3))),
                     new EncounterDelay(CharacterName: navigator.Name, Delay: _diceRoller.Roll(3)),
                     ..scouts.Select(x => new EncounterDelay(CharacterName: x.Name, Delay: _diceRoller.Roll(3)))];

            encounters.Add(new EncounterOutcome(
                IsEncounter: true,
                Terrain: terrain,
                EncounterTableRoll: _diceRoller.Roll(100),
                CanHide: pace is TravelPace.Slow,
                IsPartyTargeted: isPartyTargeted,
                TargetedForager: targetedForager.Name,
                ThreatSpotted: scouts.Any(x => _diceRoller.Roll(20) + x.PerceptionModifier + paceModifier >= duskEncounterStealth),
                EncounterDelay: encounterDelays));
        }
        else
            encounters.Add(new EncounterOutcome(IsEncounter: false, Terrain: terrain));

        return encounters;
    }
}

public record EncounterOutcome(
    bool IsEncounter, 
    TerrainType Terrain, 
    int? EncounterTableRoll = null, 
    bool? CanHide = null,
    bool? ThreatSpotted = null,
    bool? IsPartyTargeted = null,
    CharacterName? TargetedForager = null,
    IEnumerable<EncounterDelay>? EncounterDelay = null);

public record EncounterDelay(CharacterName CharacterName, int Delay);