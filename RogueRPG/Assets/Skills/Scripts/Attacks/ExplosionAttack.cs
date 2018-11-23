using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Explosion")]
public class ExplosionAttack : ConeAttack
{
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return Mathf.Abs(target.GetRow() - tile.GetRow()) <= area; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        GenerateNewAttack(user);

        foreach (Tile tile in FindObjectOfType<Battleground>().GetAvailableTiles())
        {
            if (WillBeAffected(user, target, tile))
            {
                EffectAnimation(target, skillAnimation);
                if (tile.CharacterIs(true))
                    damage.TryToDamage(user, tile.GetCharacter(), attack);
            }
        }
    }
}