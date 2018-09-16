using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Equip : ScriptableObject, IWaitForSkill
{
    [SerializeField] protected string eName;
    [SerializeField] protected SkillEffect meleeSkill;
    [SerializeField] protected SkillEffect rangedSkill;
    [SerializeField] protected SkillEffect selfSkill;
    [SerializeField] protected SkillEffect alliesSkill;
    [SerializeField] protected int hp, atk, atkm, def, defm;
    [SerializeField] protected Image backEquipPrefab;
    [SerializeField] protected Image frontEquipPrefab;
    protected Image backEquip;
    protected Image frontEquip;
    protected IWaitForEquipment requester;

    public void UseEquipmentOn(Character user, Battleground.Tile tile, IWaitForEquipment requester)
    {
        //		TODO Check if already there before adding
        user.changeEquipObject(GetBackEquip(), GetFrontEquip());
        this.requester = requester;
        AppropriateSkill(user, tile).startEffect(user, tile, this);
    }

    public SkillEffect AppropriateSkill(Character user, Battleground.Tile target)
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
            if ((Mathf.Abs(target.getIndex() - user.getPosition()) <= meleeSkill.getRange()) && target.getOccupant() != null)
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

    public string GetEquipName() { return eName; }
    public int GetHp() { return hp; }
    public int GetAtk() { return atk; }
    public int GetAtkm() { return atkm; }
    public int GetDef() { return def; }
    public int GetDefm() { return defm; }
    public SkillEffect GetMeleeEffect() { return meleeSkill; }
    public SkillEffect GetSelfEffect() { return selfSkill; }
    public SkillEffect GetRangedEffect() { return rangedSkill; }
    public SkillEffect GetAlliesEffect() { return alliesSkill; }
    public List<SkillEffect> GetAllSkillEffects()
    {
        List<SkillEffect> allSkillEffects = new List<SkillEffect>();
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