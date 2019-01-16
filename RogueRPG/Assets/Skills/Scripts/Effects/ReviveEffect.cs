using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Revive")]
public class ReviveEffect : HealEffect
{
    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);
        target.GetAttributes().GetHp().Revive((int)(user.GetAttributes().GetAtkmValue() * healIntensifier));
    }
    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return c2.GetInventory().Archetype - c1.GetInventory().Archetype;
    }
    public override bool IsLogicalTarget(Tile tile)
    {
        return tile.CharacterIs(false);
    }
    public override int GetComparableValue(Character character)
    {
        if (character.GetAttributes().GetHp().IsAlive())
            return TurnSugestion.minProbability;
        else
            return TurnSugestion.maxProbability;
    }

    public override string GetEffectDescription() { return ("Revive and heal with Atkm x " + healIntensifier); }
}
