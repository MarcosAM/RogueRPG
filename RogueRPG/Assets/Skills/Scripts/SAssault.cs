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

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    {
        if (firstEffect.UniqueEffectWillAffect(user, FindObjectOfType<Battleground>().GetTiles().Find(t => t.GetSide() != target.GetSide() && t.GetRow() == target.GetRow()), tile) || secondEffect.UniqueEffectWillAffect(user, target, tile))
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
