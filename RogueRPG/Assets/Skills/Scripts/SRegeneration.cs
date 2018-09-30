﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/Regeneration")]
public class SRegeneration : Skill
{

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        user.TryToHitWith(tile, this);
    }

    public override void OnHitEffect(Character user, Battleground.Tile tile)
    {
        base.OnHitEffect(user, tile);
        tile.getOccupant().startGeneration(Mathf.RoundToInt(value));
    }

    public override void OnMissedEffect(Character user, Battleground.Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}