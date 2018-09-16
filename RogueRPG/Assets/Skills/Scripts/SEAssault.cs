using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effects/Assault")]
public class SEAssault : Skill, IWaitForSkill
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
        //		user.getHUD ().playAnimation (this, "UseSkill");
        firstEffect.StartSkill(user, tile, this);
    }

    public override bool WillBeAffected(Battleground.Tile target, Battleground.Tile tile)
    {
        if (firstEffect.WillBeAffected(DungeonManager.getInstance().getBattleground().getMyEnemiesTiles(target.isFromHero())[target.getIndex()], tile) || secondEffect.WillBeAffected(target, tile))
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
}
