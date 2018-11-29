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
        if (precision + user.GetStatValue(Stat.Stats.Precision) > Random.value)
        {
            target.Revive((int)(user.GetStatValue(Stat.Stats.Atkm) * healIntensifier));
        }
    }
    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return (int)(c2.GetSumOfStats() - c1.GetSumOfStats());
    }
    public override bool IsLogicalTarget(Tile tile)
    {
        return tile.CharacterIs(false);
    }
    public override int GetComparableValue(Character character)
    {
        if (character.IsAlive())
            return TurnSugestion.minProbability;
        else
            return TurnSugestion.maxProbability;
    }
}
