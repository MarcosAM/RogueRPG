using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack")]
public class SAtk : Skill
{

    protected float hit;
    protected int dmg;
    protected List<int> damages = new List<int>();

    public override void StartSkill(Character user, Tile tile, IWaitForSkill requester, bool momentum)
    {
        damages.Clear();
        this.momentum = momentum;
        this.requester = requester;
        this.currentUser = user;
        this.currentTargetTile = tile;
        hit = GetHit();
        dmg = (int)GetDamage((int)value);
        PlayCastSkillAnimation();
    }

    public override void UniqueEffect(Character user, Tile tile)
    {
        if (tile.GetCharacter())
        {
            if (WasCritic())
            {
                damages.Add(Damage(tile.GetCharacter(), dmg, true));
            }
            else
            {
                if (DidIHit(tile.GetCharacter(), hit))
                {
                    damages.Add(Damage(tile.GetCharacter(), dmg, false));
                    Debug.Log("Hit!");
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
                    FindObjectOfType<Momentum>().Value += ((sum / damages.Count) * 2) / 100;
                else
                    FindObjectOfType<Momentum>().Value += (-(sum / damages.Count)) / 100;
            }
        }
        base.EndSkill();
    }

    public override bool HasHitPreview()
    {
        return true;
    }

    public override void OnHitEffect(Character user, Tile tile)
    {
        base.OnHitEffect(user, tile);
    }

    public override void OnMissedEffect(Character user, Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}