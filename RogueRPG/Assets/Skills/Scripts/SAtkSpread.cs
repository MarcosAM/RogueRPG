using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Atk and Spread")]
public class SAtkSpread : Skill
{
    [SerializeField]
    Attack attack;
    [SerializeField]
    Assist assist;

    public override bool IsTargetable(Character user, Tile tile) { return attack.IsTargetable(user, tile); }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return attack.WillBeAffected(user, target, tile) || assist.WillBeAffected(user, target, tile); }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        attack.Act(user, tile, animationPrefab);
        assist.Act(user, tile, animationPrefab);
    }
}