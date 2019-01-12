using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Self")]
public class SelfAssist : Assist
{
    public override void Act(Character user, Tile target)
    {
        effect.Affect(user, user);
    }

    public override bool IsTargetable(Character user, Tile tile) { return user == tile.GetCharacter(); }

    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        return new TurnSugestion(TurnSugestion.maxProbability - effect.GetComparableValue(user), user.GetTile().GetIndex());
    }
}