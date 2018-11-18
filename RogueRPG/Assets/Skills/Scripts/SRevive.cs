using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Revive")]
public class SRevive : Skill
{
    protected override void UniqueEffect(Character user, Tile tile)
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
}
