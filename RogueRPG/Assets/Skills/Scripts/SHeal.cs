using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Heal")]
public class SHeal : Skill
{
    public override void UniqueEffect(Character user, Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.GetCharacter() != null)
            tile.GetCharacter().Heal(Mathf.RoundToInt(value + user.GetStatValue(Stat.Stats.Atkm)));
    }
}