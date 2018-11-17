using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Revive")]
public class SRevive : Skill
{
    public override void UniqueEffect(Character user, Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.GetCharacter() != null)
        {
            if (!tile.GetCharacter().isAlive())
            {
                tile.GetCharacter().revive(Mathf.RoundToInt(tile.GetCharacter().getMaxHp() * value));
            }
        }
    }

    public override void OnHitEffect(Character user, Tile tile)
    {
        base.OnHitEffect(user, tile);

    }

    public override void OnMissedEffect(Character user, Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}
