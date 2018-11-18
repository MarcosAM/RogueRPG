using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Poison")]
public class SPoison : Skill
{

    protected override void UniqueEffect(Character user, Tile tile)
    {
        //if(tile.GetCharacter() != null)
        //    tile.GetCharacter().getPoisoned();
    }
}
