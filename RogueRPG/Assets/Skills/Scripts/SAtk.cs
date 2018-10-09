using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack")]
public class SAtk : Skill
{

    float hit;
    int dmg;
    List<int> damages = new List<int>();

    public override void StartSkill(Character user, Battleground.Tile tile, IWaitForSkill requester, bool momentum)
    {

        //base.StartSkill(user, tile, requester);
        damages.Clear();
        this.momentum = momentum;
        this.requester = requester;
        this.currentUser = user;
        this.targetTile = tile;
        hit = GetHit();
        dmg = (int)GetDamage((int)value);
        PlayCastSkillAnimation();
    }

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.getOccupant())
        {
            if (WasCritic())
            {
                damages.Add(Damage(tile.getOccupant(), dmg, true));
            }
            else
            {
                if (DidIHit(tile.getOccupant(), hit))
                {
                    damages.Add(Damage(tile.getOccupant(), dmg, false));
                }
                else
                {
                    damages.Add(0);
                    Debug.Log("Missed!");
                }
            }
        }
    }

    public override void EndSkill()
    {
        if (!momentum)
        {
            float sum = 0;
            foreach (int i in damages)
            {
                sum += i;
            }
            if (sum / damages.Count > 0)
            {
                if (currentUser.IsPlayable())
                    DungeonManager.getInstance().addMomentum((sum / damages.Count) * 2);
                else
                    DungeonManager.getInstance().addMomentum(-(sum / damages.Count));
            }
        }
        base.EndSkill();
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