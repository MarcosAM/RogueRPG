using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Ally")]
public class AllyAssist : Assist
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && tile.CharacterIs(!AffectKnockOut) && user.IsPlayable() == tile.GetSide() && user != tile.GetCharacter(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        EffectAnimation(target, skillAnimation);
        effect.Affect(user, target.GetCharacter());
    }

    public override TurnSugestion GetTurnSugestion(Character user)
    {
        List<Tile> allies = FindObjectOfType<Battleground>().GetTilesFromAliveCharactersOf(user.IsPlayable());
        allies.RemoveAll(t => IsTargetable(user, t));
        allies.RemoveAll(t => !effect.IsLogicalTarget(t));
        //TODO ver se sortbesttargets vai bugar pq buga com null
        allies.Sort((t1, t2) => effect.SortBestTargets(user, t1.GetCharacter(), t2.GetCharacter()));
        //TODO terminar isso aqui
    }
}