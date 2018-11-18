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

    protected override void UniqueEffect(Character user, Tile tile)
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

    public override TargetBtn.TargetBtnStatus GetTargetBtnStatus(Character user, Tile target, Tile tile, Equip equip)
    {
        if (UniqueEffectWillAffect(user, target, tile))
        {
            return new TargetBtn.TargetBtnStatus(equip.GetSkillColor(this), ProbabilityToHit(user, target, tile));
        }
        else
            return new TargetBtn.TargetBtnStatus();
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

    protected virtual float GetHit() { return Random.value; }

    protected virtual bool DidIHit(Character target, float attack) { return attack < ProbabilityToHit(currentUser, currentTargetTile, target.GetTile()); }

    public virtual float ProbabilityToHit(Character user, Tile target, Tile tile)
    {
        return tile.CharacterIsAlive() ? precision + user.GetStatValue(Stat.Stats.Precision) - tile.GetCharacter().GetStatValue(Stat.Stats.Dodge) : 0f;
    }

    protected virtual float GetDamage(int skillDamage)
    {
        return source == Source.Physical ? (currentUser.GetStatValue(Stat.Stats.Atk) + skillDamage) * Random.Range(1f, 1.2f) : (currentUser.GetStatValue(Stat.Stats.Atkm) + skillDamage) * Random.Range(1f, 1.2f);
    }

    protected virtual int Damage(Character user, int skillDamage, bool wasCritic) { return user.TakeDamage(skillDamage, source, wasCritic); }

    protected virtual bool WasCritic() { return source == Source.Physical ? Random.value <= critic + currentUser.GetStatValue(Stat.Stats.Critic) && critic > 0 : false; }
}