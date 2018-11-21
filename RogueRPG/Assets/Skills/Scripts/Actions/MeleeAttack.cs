using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Melee")]
public class MeleeAttack : Attack
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && tile.CharacterIsAlive() && user.IsPlayable() != tile.GetSide(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return target = tile; }

    public override void Act(Character user, Tile target)
    {
        GenerateNewAttack(user);
        if (target.CharacterIsAlive())
            damage.TryToDamage(user, target.GetCharacter(), attack);
    }
}
