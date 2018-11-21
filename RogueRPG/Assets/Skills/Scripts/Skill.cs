using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject, IWaitForAnimationByString, IWaitForAnimation
{
    public enum Source { Physical, Magic };

    [SerializeField] protected string sName;
    [SerializeField] protected Source source;
    [SerializeField] protected float value;
    [SerializeField] protected float precision;
    [SerializeField] protected float critic;
    [SerializeField] protected int range;
    [SerializeField] protected string description;
    [SerializeField] protected string castSkillAnimationTrigger;
    [SerializeField] protected float momentumValue;
    [SerializeField] protected SkillAnimation animationPrefab;
    [SerializeField] protected Actions action;
    protected bool momentum = false;
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

    protected virtual void Effect()
    {
        EffectAnimation(this.currentTargetTile);
        UniqueEffect(this.currentUser, this.currentTargetTile);
    }

    protected virtual void UniqueEffect(Character user, Tile tile) { }

    protected void EffectAnimation(Tile tile)
    {
        SkillAnimation skillAnimation = Instantiate(animationPrefab);
        skillAnimation.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        skillAnimation.PlayAnimation(this, tile.GetLocalPosition());
    }

    public virtual void ResumeFromAnimation() { EndSkill(); }

    public virtual void EndSkill()
    {
        if (!(requester is Skill) && momentum)
        {
            this.momentum = false;
        }
        requester.resumeFromSkill();
    }

    public bool IsTargetable(Character user, Tile tile) { return action.IsTargetable(user, tile); }
    //public virtual bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return target == tile ? tile.CharacterIsAlive() : false; }
    public virtual bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return action.WillBeAffected(user, target, tile); }
    public virtual TargetBtn.TargetBtnStatus GetTargetBtnStatus(Character user, Tile target, Tile tile, Equip equip)
    {
        if (UniqueEffectWillAffect(user, target, tile))
        {
            return new TargetBtn.TargetBtnStatus(equip.GetSkillColor(this));
        }
        else
            return new TargetBtn.TargetBtnStatus();
    }

    public string GetSkillName() { return sName; }
    public string GetDescription() { return description; }
    public Source GetSource() { return source; }
    public int GetRange() { return range; }
    public float GetCritic() { return critic; }
    public float GetPrecision() { return precision; }
    public virtual bool HasHitPreview() { return false; }
}
