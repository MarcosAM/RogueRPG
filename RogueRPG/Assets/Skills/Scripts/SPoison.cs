using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Poison")]
public class SPoison : Skill
{

    public override void UniqueEffect(Character user, Tile tile)
    {
        base.UniqueEffect(user, tile);
        if(tile.GetCharacter() != null)
            tile.GetCharacter().getPoisoned();
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
