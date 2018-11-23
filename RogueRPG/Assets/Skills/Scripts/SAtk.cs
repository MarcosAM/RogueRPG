using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack")]
public class SAtk : Skill
{
    [SerializeField] Attack attack;

    public override bool IsTargetable(Character user, Tile tile) { return attack.IsTargetable(user, tile); }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return attack.WillBeAffected(user.GetTile(), target, tile); }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        attack.Act(user, tile, animationPrefab);
    }
}