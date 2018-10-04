using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack")]
public class SAtk : Skill
{

    float hit;
    int dmg;

    public override void StartSkill(Character user, Battleground.Tile tile, IWaitForSkill requester)
    {
        base.StartSkill(user, tile, requester);
        hit = GetHit();
        dmg = (int)GetDamage((int)value);
        this.requester = requester;
        this.currentUser = user;
        this.targetTile = tile;
        PlayCastSkillAnimation();
    }

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.getOccupant())
        {
            if (WasCritic())
            {
                Damage(tile.getOccupant(), dmg, true);
            }
            else
            {
                if (DidIHit(tile.getOccupant(), hit))
                {
                    Damage(tile.getOccupant(), dmg, false);
                }
                else
                {
                    Debug.Log("Missed!");
                }
            }
        }
    }

    public override bool HasHitPreview()
    {
        return true;
    }

    public override void OnHitEffect(Character user, Battleground.Tile tile)
    {
        base.OnHitEffect(user, tile);
    }

    public override void OnMissedEffect(Character user, Battleground.Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}