using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[CreateAssetMenu(menuName = "Actions/Assist/Rain")]
public class RainAssist : Assist
{
    [SerializeField]
    [Range(1, 5)]
    protected int area;

    public override void Act(Character user, Tile target, SkillEffect skillEffect)
    {
        foreach (Tile tile in user.GetAlliesTiles())
        {
            if (WillBeAffected(user.GetTile(), target, tile))
            {
                skillEffect.Affect(user, tile.GetCharacter());
            }
        }
    }

    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetRow() - tile.GetRow()) <= range && user.IsPlayable() == tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return Mathf.Abs(target.GetRow() - tile.GetRow()) <= area && user.GetSide() == tile.GetSide(); }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect)
    {
        List<Tile> allies = battleground.GetTilesFromAliveCharactersOf(user.IsPlayable());
        List<Tile> possibleTargets = allies.FindAll(t => IsTargetable(user, t) && ((Effects)skillEffect).IsLogicalTarget(t));
        if (possibleTargets.Count > 0)
        {
            Dictionary<Tile, int> targetsAndAmount = new Dictionary<Tile, int>();
            foreach (Tile tile in possibleTargets)
            {
                targetsAndAmount.Add(tile, allies.FindAll(t => WillBeAffected(user.GetTile(), tile, t)).Count);
            }
            targetsAndAmount.Values.OrderByDescending(x => x);

            Tile target = GetRandomTarget(targetsAndAmount.Keys.ToList());
            return new TurnSugestion(TurnSugestion.maxProbability - (area - targetsAndAmount[target]), target.GetIndex());
        }
        else
        {
            return new TurnSugestion(0);
        }
    }

    public override string GetTargetDescription() { return ("Line " + (area * 2 + 1) + " up to " + range + " tiles"); }
}