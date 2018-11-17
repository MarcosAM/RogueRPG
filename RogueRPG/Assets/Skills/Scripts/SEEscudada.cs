using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Effects/Escudada")]
public class SEEscudada : SAtk
{
    [SerializeField] Stat.Intensity intensity;
    [SerializeField] int duration;

    public override void UniqueEffect(Character user, Tile tile)
    {
        if (tile.GetCharacter())
        {
            if (WasCritic())
            {
                damages.Add(Damage(tile.GetCharacter(), dmg, true));
            }
            else
            {
                if (DidIHit(tile.GetCharacter(), hit))
                {
                    damages.Add(Damage(tile.GetCharacter(), dmg, false));
                    var heroesTiles = DungeonManager.getInstance().getBattleground().GetAvailableTilesFrom(true);
                    if (user.getPosition() - 1 >= 0)
                    {
                        if (heroesTiles[user.getPosition() - 1].GetCharacter() != null)
                        {
                            heroesTiles[user.getPosition() - 1].GetCharacter().BuffIt(Stat.Stats.Def, intensity, duration);
                        }
                    }
                    if (user.getPosition() + 1 <= 4)
                    {
                        if (heroesTiles[user.getPosition() + 1].GetCharacter() != null)
                        {
                            heroesTiles[user.getPosition() + 1].GetCharacter().BuffIt(Stat.Stats.Def, intensity, duration);
                        }
                    }
                    Debug.Log("Hit!");
                }
                else
                {
                    damages.Add(0);
                    Debug.Log("Missed!");
                }
            }
        }
    }

    public override void OnHitEffect(Character user, Tile tile)
    {
        base.OnHitEffect(user, tile);
        //user.HitWith(tile.GetCharacter(), value, this);
    }

    public override void OnMissedEffect(Character user, Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}
