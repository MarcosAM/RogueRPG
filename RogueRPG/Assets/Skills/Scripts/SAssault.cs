using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Assault")]
public class SAssault : Skill, IWaitForSkill
{

    [SerializeField] protected Skill firstEffect;
    [SerializeField] protected Skill secondEffect;
    bool alreadyDidFirst = false;

    public override void StartSkill(Character user, Battleground.Tile tile, IWaitForSkill requester)
    {
        this.requester = requester;
        this.currentUser = user;
        this.targetTile = tile;
        alreadyDidFirst = false;
        firstEffect.StartSkill(user, tile, this);
    }

    public override bool WillBeAffected(Character user, Battleground.Tile target, Battleground.Tile tile)
    {
        if (firstEffect.WillBeAffected(user, DungeonManager.getInstance().getBattleground().getMyEnemiesTiles(target.isFromHero())[target.getIndex()], tile) || secondEffect.WillBeAffected(user, target, tile))
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
            secondEffect.StartSkill(currentUser, targetTile, this);
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
