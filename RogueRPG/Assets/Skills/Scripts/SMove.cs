using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move")]
public class SMove : Skill
{

    public override void UniqueEffect(Character user, Tile tile)
    {
        Debug.Log("Chegou aqui! Vou ter que andar para ");
        tile.SetCharacter(user);
    }
}
