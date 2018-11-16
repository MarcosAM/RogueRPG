using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Assault")]
public class SAssault : Skill, IWaitForSkill
{

    [SerializeField] protected Skill firstEffect;
    [SerializeField] protected Skill secondEffect;
    bool alreadyDidFirst = false;

    public override void StartSkill(Character user, Tile tile, IWaitForSkill requester, bool momentum)
    {
        this.momentum = momentum;
        this.requester = requester;
        this.currentUser = user;
        this.currentTargetTile = tile;
        alreadyDidFirst = false;
        firstEffect.StartSkill(user, tile, this, momentum);
    }

    public override bool WillBeAffected(Character user, Tile target, Tile tile)
    {
        //if (firstEffect.WillBeAffected(user, DungeonManager.getInstance().getBattleground().GetMyEnemiesTiles(target.isFromHero())[target.GetRow()], tile) || secondEffect.WillBeAffected(user, target, tile))
        //{
        //    return true;
        //}
        if (firstEffect.WillBeAffected(user, target.GetBattleground().GetTiles().Find(t => t.isFromHero() != target.isFromHero() && t.GetRow() == target.GetRow()), tile) || secondEffect.WillBeAffected(user, target, tile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resumeFromSkill()
    {
        if (alreadyDidFirst)
        {
            EndSkill();
        }
        else
        {
            alreadyDidFirst = true;
            secondEffect.StartSkill(currentUser, currentTargetTile, this, momentum);
        }
    }

    public override bool HasHitPreview()
    {
        if (firstEffect is SAtk || secondEffect is SAtk)
            return true;
        else
            return false;
    }
}
