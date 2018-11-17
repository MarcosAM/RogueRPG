using System.Collections;
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

    public void UseEquipmentOn(Character user, Tile tile, IWaitForEquipment requester, bool momentum)
    {
        user.changeEquipObject(GetBackEquip(), GetFrontEquip());
        this.requester = requester;
        AppropriateSkill(user, tile).StartSkill(user, tile, this, user.IsMomentumEquip(this));
    }

    public Skill AppropriateSkill(Character user, Tile target)
    {
        if (target.GetSide() == user.IsPlayable())
        {
            if (target.GetRow() == user.getPosition())
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
            if ((Mathf.Abs(target.GetRow() - user.getPosition()) <= meleeSkill.GetRange()) && target.GetCharacter() != null)
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
        requester.ResumeFromEquipment();
    }

    public virtual Tile GetBestTarget(Character user)
    {
        var possibleTargets = DungeonManager.getInstance().getBattleground().GetTilesFromAliveCharactersOf(!user.IsPlayable());
        return possibleTargets[Random.Range(0, possibleTargets.Count)];
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