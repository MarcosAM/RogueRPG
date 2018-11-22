using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Cone")]
public class ConeAttack : Attack
{
    [SerializeField]
    [Range(1, 5)]
    protected int area;
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.IsPlayable() != tile.GetSide(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return Mathf.Abs(target.GetRow() - tile.GetRow()) <= area && user.IsPlayable() != tile.GetSide(); }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        GenerateNewAttack(user);

        foreach (Tile tile in target.GetAlliesTiles())
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
