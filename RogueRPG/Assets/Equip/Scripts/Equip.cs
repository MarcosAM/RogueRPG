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

    public void UseEquipmentOn(Character user, Tile tile, IWaitForEquipment requester, bool momentum, int skill)
    {
        user.changeEquipObject(GetBackEquip(), GetFrontEquip());
        this.requester = requester;
        //AppropriateSkill(user, tile).StartSkill(user, tile, this, user.IsMomentumEquip(this));
        GetAllSkillEffects()[skill].StartSkill(user, tile, this, user.IsMomentumEquip(this));
    }

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
            if (target.GetRow() == user.GetPosition())
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
            if ((Mathf.Abs(target.GetRow() - user.GetPosition()) <= meleeSkill.GetRange()) && target.GetCharacter() != null)
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

    public Color GetSkillColor(Skill skill)
    {
        if (skill == meleeSkill)
            return new Color(0.925f, 0.258f, 0.258f, 1);
        if (skill == rangedSkill)
            return new Color(0.427f, 0.745f, 0.266f, 1);
        if (skill == selfSkill)
            return new Color(0.309f, 0.380f, 0.674f, 1);
        if (skill == alliesSkill)
            return new Color(0.952f, 0.921f, 0.235f, 1);
        return Color.black;
    }
}