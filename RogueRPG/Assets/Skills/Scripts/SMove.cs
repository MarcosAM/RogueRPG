using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move")]
public class SMove : Skill
{

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        user.getMovement().MoveTo(tile.GetRow());
    }

    public override void OnHitEffect(Character user, Battleground.Tile tile)
    {
        base.OnHitEffect(user, tile);
    }

    public override void OnMissedEffect(Character user, Battleground.Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}
