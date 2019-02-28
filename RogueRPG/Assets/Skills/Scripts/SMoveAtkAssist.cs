using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move, Attack and Assist")]
public class SMoveAtkAssist : SMoveAtk
{
    [SerializeField]
    Assist assist;

    [SerializeField]
    SkillEffect assistEffect;

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    {
        return move.WillBeAffected(user.GetTile(), target, tile) || attack.WillBeAffected(target, target.GetTileInFront(), tile) || assist.WillBeAffected(target, target, tile);
    }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        move.Act(user, tile, skillEffect);
        attack.Act(user, tile.GetTileInFront(), skillEffect);
        assist.Act(user, tile, assistEffect);
    }

    public override string GetDescription() { return description + "\n \n" + "Target: " + move.GetTargetDescription() + "\n" + "Effect: Move, " + skillEffect.GetEffectDescription() + " and " + assistEffect.GetEffectDescription(); }
}