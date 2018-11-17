﻿using System.Collections;
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
        List<Tile> tiles = FindObjectOfType<Battleground>().GetAvailableTiles().FindAll(t => WillBeAffected(currentUser, currentTargetTile, t));
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
        skillAnimation.PlayAnimation(this, tile.GetLocalPosition());
    }

    public void ResumeFromAnimation()
    {
        targetsLeft--;
        if (targetsLeft <= 0)
        {
            EndSkill();
        }
    }

    public virtual void EndSkill()
    {
        if (!(requester is Skill) && momentum)
        {
            this.momentum = false;
        }
        requester.resumeFromSkill();
    }

    public virtual bool WillBeAffected(Character user, Tile target, Tile tile) { return target == tile ? tile.CharacterIsAlive() : false; }

    public string GetSkillName() { return sName; }
    public string GetDescription() { return description; }
    public Source GetSource() { return source; }
    public virtual void UniqueEffect(Character user, Tile tile) { }
    public int GetRange() { return range; }
    public float GetCritic() { return critic; }
    public float GetPrecision() { return precision; }
    public virtual bool HasHitPreview() { return false; }
}
