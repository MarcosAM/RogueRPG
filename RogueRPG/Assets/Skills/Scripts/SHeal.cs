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

    public override void OnHitEffect(Character user, Tile tile)
    {
        base.OnHitEffect(user, tile);
    }

    public override void OnMissedEffect(Character user, Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}