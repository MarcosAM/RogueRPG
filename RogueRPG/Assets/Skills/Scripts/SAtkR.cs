using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack Ranged")]
public class SAtkR : SAtk
{
    //public override float ProbabilityToHit(Character user, Tile target, Tile tile)
    //{
    //    float distanceInfluence;
    //    if (Mathf.Abs(user.GetPosition() - target.GetRow()) <= range)
    //    {
    //        distanceInfluence = 0;
    //    }
    //    else
    //    {
    //        distanceInfluence = Mathf.Abs(user.GetPosition() - target.GetRow()) * 0.1f;
    //    }
    //    return tile.CharacterIsAlive() ? precision + user.GetStatValue(Stat.Stats.Precision) - distanceInfluence - tile.GetCharacter().GetStatValue(Stat.Stats.Dodge) : 0f;
    //}
}
