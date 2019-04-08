using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack and Move")]
public class SAtkMove : Movement
{
    [SerializeField]
    Attack attack;

    public override bool IsTargetable(Character user, Tile tile) { return move.IsTargetable(user, tile); }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    {
        return move.WillBeAffected(user.GetTile(), target, tile) || attack.WillBeAffected(user.GetTile(), user.GetTile().GetTileInFront(), tile);
    }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        attack.Act(user, user.GetTile().GetTileInFront(), skillEffect);
        move.Act(user, tile, skillEffect);
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        if (user.GetAttributes().GetHP() < user.GetAttributes().GetMaxHP() * .35F)
        {
            var enemiesTiles = battleground.GetTilesFromAliveCharactersOf(!user.Playable);
            var tiles = battleground.GetAvailableTilesFrom(user.Playable);

            tiles.Sort((t1, t2) => GetAllDistances(t2, enemiesTiles) - GetAllDistances(t1, enemiesTiles));

            if (GetAllDistances(user.GetTile(), enemiesTiles) == GetAllDistances(tiles[0], enemiesTiles))
            {
                return new TurnSugestion(0);
            }
            else
            {
                return new TurnSugestion(2, tiles[0].GetIndex());
            }
        }

        return move.GetTurnSugestion(user, battleground, skillEffect);
    }

    int GetAllDistances(Tile tile, List<Tile> tiles)
    {
        var distances = 0;

        foreach (var t in tiles)
        {
            distances += CombatRules.GetDistance(tile, t);
        }

        return distances;
    }

    public override string GetDescription() { return description + "\n \n" + "Target: " + move.GetTargetDescription() + "\n" + "Effect: " + skillEffect.GetEffectDescription() + " and Move"; }
}
