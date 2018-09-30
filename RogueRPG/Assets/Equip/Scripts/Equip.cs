﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Equip : ScriptableObject, IWaitForSkill
{
    [SerializeField] protected string eName;
    [SerializeField] protected Skill meleeSkill;
    [SerializeField] protected Skill rangedSkill;
    [SerializeField] protected Skill selfSkill;
    [SerializeField] protected Skill alliesSkill;
    [SerializeField] protected int hp, atk, atkm, def, defm;
    [SerializeField] protected Image backEquipPrefab;
    [SerializeField] protected Image frontEquipPrefab;
    protected Image backEquip;
    protected Image frontEquip;
    protected IWaitForEquipment requester;

    public void UseEquipmentOn(Character user, Battleground.Tile tile, IWaitForEquipment requester)
    {
        user.changeEquipObject(GetBackEquip(), GetFrontEquip());
        this.requester = requester;
        AppropriateSkill(user, tile).StartSkill(user, tile, this);
    }

    public Skill AppropriateSkill(Character user, Battleground.Tile target)
    {
        if (target.isFromHero() == user.isPlayable())
        {
            if (target.getIndex() == user.getPosition())
            {
                return selfSkill;
            }
            else
            {
                return alliesSkill;
            }
        }
        else
        {
            if ((Mathf.Abs(target.getIndex() - user.getPosition()) <= meleeSkill.GetRange()) && target.getOccupant() != null)
            {
                return meleeSkill;
            }
            else
            {
                return rangedSkill;
            }
        }
    }

    public void resumeFromSkill()
    {
        requester.resumeFromEquipment();
    }

    public virtual Battleground.Tile GetBestTarget(Character user)
    {
        Battleground.Tile[] possibleTargets = DungeonManager.getInstance().getBattleground().GetAliveOpponents(user);
        return possibleTargets[Random.Range(0, possibleTargets.Length)];
    }

    public string GetEquipName() { return eName; }
    public int GetHp() { return hp; }
    public int GetAtk() { return atk; }
    public int GetAtkm() { return atkm; }
    public int GetDef() { return def; }
    public int GetDefm() { return defm; }
    public Skill GetMeleeEffect() { return meleeSkill; }
    public Skill GetSelfEffect() { return selfSkill; }
    public Skill GetRangedEffect() { return rangedSkill; }
    public Skill GetAlliesEffect() { return alliesSkill; }
    public List<Skill> GetAllSkillEffects()
    {
        List<Skill> allSkillEffects = new List<Skill>();
        if (meleeSkill != null)
            allSkillEffects.Add(meleeSkill);
        if (rangedSkill != null)
            allSkillEffects.Add(rangedSkill);
        if (selfSkill != null)
            allSkillEffects.Add(selfSkill);
        if (alliesSkill != null)
            allSkillEffects.Add(alliesSkill);
        return allSkillEffects;
    }
    public Image GetBackEquip()
    {
        if (backEquipPrefab != null)
        {
            if (backEquip == null)
            {
                backEquip = Instantiate(backEquipPrefab);
            }
            return backEquip;
        }
        else
        {
            return null;
        }
    }
    public Image GetFrontEquip()
    {
        if (frontEquipPrefab != null)
        {
            if (frontEquip == null)
            {
                frontEquip = Instantiate(frontEquipPrefab);
            }
            return frontEquip;
        }
        else
        {
            return null;
        }
    }
}