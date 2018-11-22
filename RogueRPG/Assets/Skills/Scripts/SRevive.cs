using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Revive")]
public class SRevive : SBuff
{
    //protected override void UniqueEffect(Character user, Tile tile)
    //{
    //    base.UniqueEffect(user, tile);
    //    if (tile.GetCharacter() != null)
    //    {
    //        if (!tile.GetCharacter().IsAlive())
    //        {
    //            tile.GetCharacter().Revive(Mathf.RoundToInt(tile.GetCharacter().GetMaxHp() * value));
    //        }
    //    }
    //}
}
