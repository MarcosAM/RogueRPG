using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move")]
public class SMove : Skill
{

    public override void UniqueEffect(Character user, Tile tile)
    {
        base.UniqueEffect(user, tile);
        Debug.Log("Chegou aqui! Vou ter que andar para ");
        tile.SetCharacter(user);
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
