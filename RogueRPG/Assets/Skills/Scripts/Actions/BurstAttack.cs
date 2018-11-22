﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Burst")]
public class BurstAttack : Attack
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.GetTile() != tile; }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return IsTargetable(user, tile); }
    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        GenerateNewAttack(user);

        foreach (Tile tile in FindObjectOfType<Battleground>().GetAvailableTiles())
        {
            if (WillBeAffected(user, target, tile))
            {
                EffectAnimation(target, skillAnimation);
                if (tile.CharacterIsAlive())
                    damage.TryToDamage(user, tile.GetCharacter(), attack);
            }
        }
    }
}
