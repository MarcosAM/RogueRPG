using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject, IWaitForAnimationByString
{
    [SerializeField] protected string sName;
    [SerializeField] protected string description;
    [SerializeField] protected string castSkillAnimationTrigger;
    [SerializeField] protected SkillEffect skillEffect;
    protected Character currentUser;
    protected Tile currentTargetTile;
    protected IWaitForSkill requester;

    public virtual void StartSkill(Character user, Tile tile, IWaitForSkill requester)
    {
        SkillName.ShowSkillName(sName);
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
        UniqueEffect(this.currentUser, this.currentTargetTile);
        requester.resumeFromSkill();
    }
    protected abstract void UniqueEffect(Character user, Tile tile);
    public abstract bool IsTargetable(Character user, Tile tile);
    public abstract bool UniqueEffectWillAffect(Character user, Tile target, Tile tile);

    public string GetSkillName() { return sName; }
    public abstract string GetDescription();
    public abstract TurnSugestion GetTurnSugestion(Character user, Battleground battleground);
}