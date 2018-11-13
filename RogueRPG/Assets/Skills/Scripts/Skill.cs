using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject, IWaitForAnimationByString, IWaitForAnimation
{
    public enum Type { Melee, Ranged }
    public enum Source { Physical, Magic };
    public enum Kind { Offensive, Heal, Buff, Debuff, Movement };

    [SerializeField] protected string sName;
    [SerializeField] protected Type type;
    [SerializeField] protected Source source;
    [SerializeField] protected Kind kind;
    [SerializeField] protected float value;
    [SerializeField] protected float precision;
    [SerializeField] protected float critic;
    [SerializeField] protected int range;
    [SerializeField] protected bool singleTarget;
    [SerializeField] protected bool hitsDead;
    [SerializeField] protected string description;
    [SerializeField] protected string castSkillAnimationTrigger;
    [SerializeField] protected float momentumValue;
    [SerializeField] protected SkillAnimation animationPrefab;
    protected bool momentum = false;
    protected int targetsLeft;
    protected Character currentUser;
    protected Tile currentTargetTile;
    protected IWaitForSkill requester;

    public virtual void StartSkill(Character user, Tile tile, IWaitForSkill requester, bool momentum)
    {
        this.momentum = momentum;
        this.requester = requester;
        this.currentUser = user;
        this.currentTargetTile = tile;
        PlayCastSkillAnimation();
    }

    protected void PlayCastSkillAnimation()
    {
        if (castSkillAnimationTrigger != null)
        {
            currentUser.PlayAnimation(this, castSkillAnimationTrigger);
        }
    }

    public virtual void ResumeFromAnimation(IPlayAnimationByString animationByString)
    {
        Effect();
    }

    void Effect()
    {
        //if (singleTarget)
        //{
        //    EffectAnimation(currentTargetTile);
        //    UniqueEffect(currentUser, currentTargetTile);
        //    return;
        //}
        //else
        //{
        //    targetsLeft = currentTargetTile.GetAlliesTiles().Length;
        //    foreach (Tile t in currentTargetTile.GetAlliesTiles())
        //    {
        //        EffectAnimation(t);
        //        UniqueEffect(currentUser, t);
        //    }
        //}
        List<Tile> tiles = FindObjectOfType<Battleground>().GetTiles().FindAll(t => WillBeAffected(currentUser, currentTargetTile, t));
        targetsLeft = tiles.Count;
        foreach (Tile tile in tiles)
        {
            EffectAnimation(tile);
            UniqueEffect(currentUser, tile);
        }
    }

    public void EffectAnimation(Tile tile)
    {
        SkillAnimation skillAnimation = Instantiate(animationPrefab);
        skillAnimation.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        skillAnimation.PlayAnimation(this, tile.getLocalPosition());
    }

    public void ResumeFromAnimation()
    {
        //if (singleTarget)
        //{
        //    EndSkill();
        //}
        //else
        //{
        targetsLeft--;
        if (targetsLeft <= 0)
        {
            EndSkill();
        }
        //}
    }

    public virtual void EndSkill()
    {
        if (!(requester is Skill) && momentum)
        {
            FindObjectOfType<Momentum>().OnMomentumSkillUsed();
            this.momentum = false;
        }
        requester.resumeFromSkill();
    }

    protected float GetHit()
    {
        return Random.value;
    }

    protected bool DidIHit(Character target, float attack)
    {
        return attack < ProbabilityToHit(currentUser, currentTargetTile, target.GetTile());
    }

    public float ProbabilityToHit(Character user, Tile target, Tile tile)
    {
        float distanceInfluence;
        if (type == Skill.Type.Melee)
        {
            distanceInfluence = 0;
        }
        else
        {
            if (singleTarget)
            {
                if (Mathf.Abs(user.getPosition() - target.GetRow()) <= range)
                {
                    distanceInfluence = 0;
                }
                else
                {
                    distanceInfluence = Mathf.Abs(user.getPosition() - target.GetRow()) * 0.1f;
                }
            }
            else
            {
                if (Mathf.Abs(target.GetRow() - tile.GetRow()) <= range)
                {
                    distanceInfluence = 0;
                }
                else
                {
                    distanceInfluence = Mathf.Abs(target.GetRow() - tile.GetRow()) * 0.1f;
                }
            }
        }
        if (tile.IsOccupied())
        {
            return precision + user.GetStatValue(Stat.Stats.Precision) - distanceInfluence - tile.getOccupant().GetStatValue(Stat.Stats.Dodge);
        }
        else
            return 0f;
    }

    protected float GetDamage(int skillDamage)
    {
        if (source == Source.Physical)
        {
            return (currentUser.GetStatValue(Stat.Stats.Atk) + skillDamage) * Random.Range(1f, 1.2f);
        }
        else
        {
            return (currentUser.GetStatValue(Stat.Stats.Atkm) + skillDamage) * Random.Range(1f, 1.2f);
        }
    }

    protected int Damage(Character user, int skillDamage, bool wasCritic)
    {
        return user.takeDamage(skillDamage, source, wasCritic);
    }

    protected bool WasCritic()
    {
        if (Random.value <= critic + currentUser.GetStatValue(Stat.Stats.Critic) && critic > 0 && source == Source.Physical)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool WillBeAffected(Character user, Tile target, Tile tile)
    {
        if (target == tile)
        {
            if (tile.IsOccupied())
            {
                return true;
            }
            else
            {
                if ((type == Type.Ranged && !singleTarget) || (kind == Kind.Movement && Mathf.Abs(user.getPosition() - tile.GetRow()) <= range))
                    return true;
                else
                    return false;
            }
        }
        else
        {
            if (singleTarget)
            {
                return false;
            }
            else
            {
                if (type == Type.Melee)
                {
                    if (Mathf.Abs(target.GetRow() - tile.GetRow()) <= range)
                    {
                        if (target.isFromHero() == tile.isFromHero())
                        {
                            if (tile.getOccupant() != null)
                            {
                                if (tile.getOccupant().isAlive() || hitsDead)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (target.isFromHero() == tile.isFromHero())
                    {
                        if (tile.getOccupant() != null)
                        {
                            if (tile.getOccupant().isAlive() || hitsDead)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }

    public string GetSkillName() { return sName; }
    public string GetDescription() { return description; }
    public Source GetSource() { return source; }
    public bool DoesTargetDead() { return hitsDead; }
    public virtual void UniqueEffect(Character user, Tile tile) { }
    public virtual void OnHitEffect(Character user, Tile tile) { }
    public virtual void OnMissedEffect(Character user, Tile tile) { }
    public int GetRange() { return range; }
    public Type GetSkillType() { return type; }
    public Kind GetKind() { return kind; }
    public float GetCritic() { return critic; }
    public float GetPrecision() { return precision; }
    public bool IsSingleTarget() { return singleTarget; }
    public virtual bool HasHitPreview() { return false; }
}
