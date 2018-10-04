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
    protected int targetsLeft;
    protected Character currentUser;
    protected Battleground.Tile targetTile;
    protected IWaitForSkill requester;

    public virtual void StartSkill(Character user, Battleground.Tile tile, IWaitForSkill requester)
    {
        this.requester = requester;
        this.currentUser = user;
        this.targetTile = tile;
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
        if (singleTarget)
        {
            EffectAnimation(targetTile);
            UniqueEffect(currentUser, targetTile);
            return;
        }
        else
        {
            targetsLeft = targetTile.GetAlliesTiles().Length;
            foreach (Battleground.Tile t in targetTile.GetAlliesTiles())
            {
                EffectAnimation(t);
                UniqueEffect(currentUser, t);
            }
        }
    }

    public void EffectAnimation(Battleground.Tile tile)
    {
        SkillAnimation skillAnimation = Instantiate(animationPrefab);
        skillAnimation.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        skillAnimation.PlayAnimation(this, tile.getLocalPosition());
    }

    public void ResumeFromAnimation()
    {
        if (singleTarget)
        {
            EndSkill();
        }
        else
        {
            targetsLeft--;
            if (targetsLeft <= 0)
            {
                EndSkill();
            }
        }
    }

    public void EndSkill()
    {
        requester.resumeFromSkill();
    }

    protected float GetHit()
    {
        return Random.value;
    }

    protected bool DidIHit(Character target, float attack)
    {
        return attack < ProbabilityToHit(currentUser, targetTile, target.GetTile());
    }

    //protected float AttackValue(Character target, float attack)
    //{
    //    if (type == Skill.Type.Melee)
    //    {
    //        return attack;
    //    }
    //    else
    //    {
    //        if (singleTarget)
    //        {
    //            return attack + (Mathf.Abs(currentUser.getPosition() - target.getPosition()) * 0.1f) + target.getDodgeValue();
    //        }
    //        else
    //        {
    //            return attack + (Mathf.Abs(this.targetTile.getIndex() - target.getPosition()) * 0.1f) + target.getDodgeValue();
    //        }
    //    }
    //}

    public float ProbabilityToHit(Character user, Battleground.Tile target, Battleground.Tile tile)
    {
        float distanceInfluence;
        if (type == Skill.Type.Melee)
        {
            distanceInfluence = 0;
        }
        else
        {
            //if (Mathf.Abs(user.getPosition() - target.getPosition()) <= range)
            //{
            //    distanceInfluence = 0;
            //}
            //else
            //{
            if (singleTarget)
            {
                if (Mathf.Abs(user.getPosition() - target.getIndex()) <= range)
                {
                    distanceInfluence = 0;
                }
                else
                {
                    distanceInfluence = Mathf.Abs(user.getPosition() - target.getIndex()) * 0.1f;
                }
            }
            else
            {
                if (Mathf.Abs(target.getIndex() - tile.getIndex()) <= range)
                {
                    distanceInfluence = 0;
                }
                else
                {
                    distanceInfluence = Mathf.Abs(target.getIndex() - tile.getIndex()) * 0.1f;
                }
            }
            //}
        }
        if (tile.IsOccupied())
        {
            return precision + user.getPrecisionValue() - distanceInfluence - tile.getOccupant().getDodgeValue();
        }
        else
            return 0f;
    }

    protected float GetDamage(int skillDamage)
    {
        if (source == Source.Physical)
        {
            return (currentUser.getAtkValue() + skillDamage) * Random.Range(1f, 1.2f);
        }
        else
        {
            return (currentUser.getAtkmValue() + skillDamage) * Random.Range(1f, 1.2f);
        }
    }

    protected int Damage(Character user, int skillDamage, bool wasCritic)
    {
        return user.takeDamage(skillDamage, source, wasCritic);
    }

    protected bool WasCritic()
    {
        if (Random.value <= critic + currentUser.getCritic().getValue() && critic > 0 && source == Source.Physical)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool WillBeAffected(Character user, Battleground.Tile target, Battleground.Tile tile)
    {
        if (target == tile)
        {
            if (tile.IsOccupied())
            {
                return true;
            }
            else
            {
                if ((type == Type.Ranged && !singleTarget) || (kind == Kind.Movement && Mathf.Abs(user.getPosition() - tile.getIndex()) <= range))
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
                    if (Mathf.Abs(target.getIndex() - tile.getIndex()) <= range)
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
    public virtual void UniqueEffect(Character user, Battleground.Tile tile) { }
    public virtual void OnHitEffect(Character user, Battleground.Tile tile) { }
    public virtual void OnMissedEffect(Character user, Battleground.Tile tile) { }
    public int GetRange() { return range; }
    public Type GetSkillType() { return type; }
    public Kind GetKind() { return kind; }
    public float GetCritic() { return critic; }
    public float GetPrecision() { return precision; }
    public bool IsSingleTarget() { return singleTarget; }
    public virtual bool HasHitPreview() { return false; }
}
