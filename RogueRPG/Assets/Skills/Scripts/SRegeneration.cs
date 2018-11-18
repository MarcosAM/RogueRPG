using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/Regeneration")]
public class SRegeneration : Skill
{

    protected override void UniqueEffect(Character user, Tile tile)
    {
        tile.GetCharacter().startGeneration(Mathf.RoundToInt(value));
    }
}
