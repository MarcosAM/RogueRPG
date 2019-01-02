using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Revive")]
public class ReviveEffect : HealEffect
{
    [Range(0f, 1f)]
    [SerializeField]
    float precision;

    public override void Affect(Character user, Character target)
    {
        if (precision + user.GetAttributes().GetStatValue(Attribute.Stats.Precision) > Random.value)
        {
            target.GetAttributes().Revive((int)(user.GetAttributes().GetStatValue(Attribute.Stats.Atkm) * healIntensifier));
        }
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
        if (character.GetAttributes().IsAlive())
            return TurnSugestion.minProbability;
        else
            return TurnSugestion.maxProbability;
    }
}
