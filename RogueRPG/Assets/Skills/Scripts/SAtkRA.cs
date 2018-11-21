using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack Ranged All")]
public class SAtkRA : SAtkR
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
    //    if (target == tile)
    //        return true;
    //    return target.GetSide() == tile.GetSide() /*&& tile.CharacterIsAlive()*/;
    //}

    //public override float ProbabilityToHit(Character user, Tile target, Tile tile)
    //{
    //    float distanceInfluence;
    //    if (Mathf.Abs(target.GetRow() - tile.GetRow()) <= range)
    //    {
    //        distanceInfluence = 0;
    //    }
    //    else
    //    {
    //        distanceInfluence = Mathf.Abs(target.GetRow() - tile.GetRow()) * 0.1f;
    //    }
    //    return tile.CharacterIsAlive() ? precision + user.GetStatValue(Stat.Stats.Precision) - distanceInfluence - tile.GetCharacter().GetStatValue(Stat.Stats.Dodge) : 0f;
    //}
}
