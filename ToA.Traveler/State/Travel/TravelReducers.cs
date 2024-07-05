using Fluxor;
using ToA.Traveler.Models;

namespace ToA.Traveler.State.Travel;

public static class TravelReducers
{
    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.DayStarted action)
    {
        
        var theParty = state.TheParty?.ToList() ?? Characters.TheParty.ToList();

        if (state.TheParty is not null)
        {
            foreach (var character in state.TheParty)
            {
                theParty.Remove(character);

                if (character.ConsumedRations >= 1 && character.ConsumedWater >= (state.WeatherReport!.Temperature is Temperature.ExtremeHeat ? 3M : 2M))
                    character.Exhaustion = Math.Max(0, character.Exhaustion - 1);

                character.DaysWithoutFood += 1;
                character.ConsumedWater = 0;
                character.ConsumedRations = 0;

                theParty.Add(character);
            }
        }

        return state with
        {
            Day = state.Day + 1,
            IsCamping = false,
            IsNavigating = false,
            IsDayEnded = false,
            IsTraveling = false,
            IsConfirmingTravelJobs = false,
            IsConfirmingTravelSpecs = false,
            PreviousNavigationOutcome = state.NavigationOutcome ?? new(IsPartyLost: false, UsedCartographers: false, IsNavigatorOutlander: false, TravelDirection.North, HexCount: 0),
            WeatherReport = null,
            NavigationOutcome = null,
            EncounterOutcomes = null,
            ForageOutcomes = null,
            HydrationOutcomes = null,
            StarvationOutcomes = null,   
            TropicalStormOutcomes = null,
            TheParty = theParty,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.TravelStarted action) =>
        state with
        {
            IsTraveling = true,
        };

    [ReducerMethod]
    public static TravelState Reduce(
       TravelState state,
       TravelActions.ConfirmTravelJobs action) =>
       state with
       {
           IsConfirmingTravelJobs = true,
       };

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.WeatherReported action) =>
        state with
        {
            WeatherReport = action.WeatherReport,
        };

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.ConfirmTravelSpecs action)
    {
        return state with
        {
            IsConfirmingTravelSpecs = true,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.TravelJobsConfirmed action)
    {
        return state with
        {
            TheParty = action.TheParty.ToList(),
            IsConfirmingTravelJobs = false,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.TravelSpecsConfirmed action) =>
        state with
        {
            TravelPace = action.TravelPace,
            TravelDirection = action.TravelDirection,
            TravelTerrain = action.TravelTerrain,
            IsConfirmingTravelSpecs = false,
        };

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.StartNavigation action)
    {
        var theParty = state.TheParty.ToList();

        foreach (var character in state.TheParty)
        {
            theParty.Remove(character);
            character.DaysWithoutFood += 1;
            theParty.Add(character);
        }

        return state with
        {
            TheParty = theParty,
            IsNavigating = true,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.EndNavigation action)
    {
        return state with
        {
            IsNavigating = false,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.NavigationStarted action) =>
        state with
        {
            NavigationOutcome = action.Outcome,
        };

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.EncountersRolled action) =>
        state with
        {
            EncounterOutcomes = action.Encounters.ToList(),
        };

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.ForagingStarted action) =>
        state with
        {
            ForageOutcomes = action.Outcomes.ToList(),
        };

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.HydrationChecked action)
    {
        var theParty = state.TheParty.ToList();

        foreach (var outcome in action.Outcomes)
        {
            var thePartyCharacter = theParty.Single(x => x.Name == outcome.CharacterName);
            theParty.Remove(thePartyCharacter);
            thePartyCharacter.Exhaustion += outcome.ExhaustionGained;
            theParty.Add(thePartyCharacter);
        }

        return state with
        {
            TheParty = theParty,
            HydrationOutcomes = action.Outcomes.ToList(),
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.StarvationChecked action)
    {
        var theParty = state.TheParty.ToList();

        foreach (var outcome in action.Outcomes)
        {
            var thePartyCharacter = theParty.Single(x => x.Name == outcome.CharacterName);
            theParty.Remove(thePartyCharacter);
            thePartyCharacter.Exhaustion += outcome.ExhaustionGained;
            theParty.Add(thePartyCharacter);
        }

        return state with
        {
            TheParty = theParty,
            StarvationOutcomes = action.Outcomes.ToList(),
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.TropicalStormChecked action)
    {
        var theParty = state.TheParty.ToList();

        foreach (var outcome in action.Outcomes)
        {
            var thePartyCharacter = theParty.Single(x => x.Name == outcome.CharacterName);
            theParty.Remove(thePartyCharacter);
            thePartyCharacter.Exhaustion += outcome.ExhaustionGained;
            theParty.Add(thePartyCharacter);
        }

        return state with
        {
            TheParty = theParty,
            TropicalStormOutcomes = action.Outcomes.ToList(),
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.ConsumeFood action)
    {
        var theParty = state.TheParty.ToList();
        var character = state.TheParty.Single(x => x.Name == action.CharacterName);

        theParty.Remove(character);
        character.DaysWithoutFood = 0;
        character.ConsumedRations += action.Pounds;
        theParty.Add(character);

        return state with
        {
            TheParty = theParty,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.ConsumeWater action)
    {
        var theParty = state.TheParty.ToList();
        var character = state.TheParty.Single(x => x.Name == action.CharacterName);

        theParty.Remove(character);
        character.ConsumedWater += action.Gallons;
        theParty.Add(character);

        return state with
        {
            TheParty = theParty,
        };
    }

    [ReducerMethod]
    public static TravelState Reduce(
        TravelState state,
        TravelActions.SwapArmor action)
    {
        var theParty = state.TheParty.ToList();
        var character = state.TheParty.Single(x => x.Name == action.CharacterName);

        theParty.Remove(character);
        character.Armor = action.ArmorType;
        theParty.Add(character);

        return state with
        {
            TheParty = theParty,
        };
    }
}
