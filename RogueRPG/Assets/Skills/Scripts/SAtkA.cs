using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack All")]
public class SAtkA : SAtk
{
    //protected int targetsLeft;

    //protected override void Effect()
    //{
    //    List<Tile> tiles = FindObjectOfType<Battleground>().GetAvailableTiles().FindAll(t => UniqueEffectWillAffect(currentUser, currentTargetTile, t));
    //    targetsLeft = tiles.Count;
    //    foreach (Tile tile in tiles)
    //    {
    //        EffectAnimation(tile);
    //        UniqueEffect(currentUser, tile);
    //    }
    //}

    //public override void ResumeFromAnimation()
    //{
    //    targetsLeft--;
    //    if (targetsLeft <= 0)
    //    {
    //        EndSkill();
    //    }
    //}

    //public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    //{
    //    return target.GetSide() == tile.GetSide() ? Mathf.Abs(target.GetRow() - tile.GetRow()) <= range && tile.CharacterIsAlive() : false;
    //}
}
