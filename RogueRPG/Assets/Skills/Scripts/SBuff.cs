using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Buff or Debuff")]
public class SBuff : Skill
{
    [SerializeField] Assist assist;

    protected override void UniqueEffect(Character user, Tile tile)
    {
        assist.Act(user, tile, animationPrefab);
    }
    public override bool IsTargetable(Character user, Tile tile) { return assist.IsTargetable(user, tile); }
    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return assist.WillBeAffected(user, target, tile); }
    public Assist GetAssist() { return assist; }
}