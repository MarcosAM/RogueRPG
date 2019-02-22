using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Wear Skill")]
public class WearSkill : Skill, IWaitForSkill
{
    [SerializeField] Skill skill;

    public override void StartSkill(Character user, Tile tile, IWaitForSkill requester)
    {
        this.requester = requester;
        this.currentUser = user;
        this.currentTargetTile = tile;

        skill.StartSkill(user, tile, this);
    }

    public void resumeFromSkill()
    {
        currentUser.GetAttributes().GetHp().LoseHpBy(Mathf.RoundToInt(currentUser.GetAttributes().GetHp().GetMaxValue() * 0.1f), false);
        requester.resumeFromSkill();
    }

    public override string GetDescription()
    {
        //TODO universializar as descrições de skills apra que elas se encaixem com isso:
        return "User loses some hp so he can do " + skill.GetDescription();
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        if (user is Plant)
            return new TurnSugestion(5, user.GetTile().GetTileInFront().GetIndex());
        else
            return skill.GetTurnSugestion(user, battleground);
    }

    public override bool IsTargetable(Character user, Tile tile)
    {
        return skill.IsTargetable(user, tile);
    }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    {
        return skill.UniqueEffectWillAffect(user, target, tile);
    }

    protected override void UniqueEffect(Character user, Tile tile) { }
}
