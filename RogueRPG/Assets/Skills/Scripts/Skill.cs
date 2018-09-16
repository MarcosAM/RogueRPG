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
    [SerializeField] protected bool hitsTile;
    [SerializeField] protected bool hitsDead;
    [SerializeField] protected string description;
    [SerializeField] protected string castSkillAnimationTrigger;
    [SerializeField] protected float momentumValue;
    [SerializeField] protected SkillAnimation animationPrefab;
    protected int howManyTargets;
    protected int targetsHited;
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
            Battleground.Tile[] targets;
            if (targetTile.isFromHero() == currentUser.isPlayable())
            {
                targets = DungeonManager.getInstance().getBattleground().getMySideTiles(currentUser.isPlayable());
            }
            else
            {
                targets = DungeonManager.getInstance().getBattleground().getMyEnemiesTiles(currentUser.isPlayable());
            }
            howManyTargets = targets.Length;
            targetsHited = 0;
            foreach (Battleground.Tile t in targets)
            {
                EffectAnimation(t);
                if (targetTile.isFromHero() == currentUser.isPlayable())
                {
                    if (targetTile.getOccupant() == currentUser)
                    {
                        UniqueEffect(currentUser, t);
                    }
                    else
                    {
                        UniqueEffect(currentUser, t);
                    }
                }
                else
                {
                    UniqueEffect(currentUser, t);
                }
            }
        }
    }

    public void EndSkill()
    {
        requester.resumeFromSkill();
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
            targetsHited++;
            if (targetsHited >= howManyTargets)
            {
                EndSkill();
            }
        }
    }

    protected float GetAttack()
    {
        return Random.value - precision - currentUser.getPrecision().getValue();
    }

    protected bool DidIHit(Character target, float attack)
    {
        if (type == Skill.Type.Melee)
        {
            return target.didIHitYou(attack);
        }
        else
        {
            return target.didIHitYou(attack - Mathf.Abs(currentUser.getPosition() - target.getPosition()));
        }
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

    public virtual bool WillBeAffected(Battleground.Tile target, Battleground.Tile tile)
    {
        if (target == tile)
        {
            return true;
        }
        else
        {
            if (singleTarget)
            {
                return false;
            }
            else
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
        }
    }

    public string GetSkillName() { return sName; }
    public string GetDescription() { return description; }
    public Source GetSource() { return source; }
    public bool DoesTargetTile() { return hitsTile; }
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
}
