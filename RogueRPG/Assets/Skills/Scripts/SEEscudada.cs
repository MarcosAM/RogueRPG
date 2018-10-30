using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effects/Escudada")]
public class SEEscudada : Skill
{
    [SerializeField] Stat.Intensity intensity;
    [SerializeField] int duration;

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        //		if (tile.getOccupant ())
        user.TryToHitWith(tile, this);
    }

    public override void OnHitEffect(Character user, Battleground.Tile tile)
    {
        base.OnHitEffect(user, tile);
        user.HitWith(tile.getOccupant(), value, this);
        Battleground.Tile[] heroesTiles = DungeonManager.getInstance().getBattleground().getHeroesTiles();
        if (user.getPosition() - 1 >= 0)
        {
            if (heroesTiles[user.getPosition() - 1].getOccupant() != null)
            {
                heroesTiles[user.getPosition() - 1].getOccupant().BuffIt(Stat.Stats.Def, intensity, duration);
            }
        }
        if (user.getPosition() + 1 <= 4)
        {
            if (heroesTiles[user.getPosition() + 1].getOccupant() != null)
            {
                heroesTiles[user.getPosition() + 1].getOccupant().BuffIt(Stat.Stats.Def, intensity, duration);
            }
        }
    }

    public override void OnMissedEffect(Character user, Battleground.Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}
