﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Burst")]
public class BurstAttack : Attack
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.GetTile() != tile; }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return IsTargetable(user.GetCharacter(), tile); }
    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        GenerateNewAttack(user);

        foreach (Tile tile in FindObjectOfType<Battleground>().GetAvailableTiles())
        {
            if (WillBeAffected(user.GetTile(), target, tile))
            {
                EffectAnimation(target, skillAnimation);
                if (tile.CharacterIs(true))
                    damage.TryToDamage(user, tile.GetCharacter(), attack);
            }
        }
    }

    public override TurnSugestion GetTurnSugestion(Character user)
    {
        List<Tile> enemies = FindObjectOfType<Battleground>().GetTilesFromAliveCharactersOf(!user.IsPlayable());
        List<Tile> allies = FindObjectOfType<Battleground>().GetTilesFromAliveCharactersOf(user.IsPlayable());
        enemies.RemoveAll(t => !IsTargetable(user, t));
        allies.RemoveAll(t => !IsTargetable(user, t));
        if (enemies.Count > 0)
            return new TurnSugestion(TurnSugestion.maxProbability - allies.Count, enemies[0].GetIndex());
        else
            return new TurnSugestion(0);
    }
}