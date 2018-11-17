using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack All")]
public class SAtkA : SAtk
{
    public override bool WillBeAffected(Character user, Tile target, Tile tile)
    {
        return target.GetSide() == tile.GetSide() ? Mathf.Abs(target.GetRow() - tile.GetRow()) <= range && tile.CharacterIsAlive() : false;
    }
}
