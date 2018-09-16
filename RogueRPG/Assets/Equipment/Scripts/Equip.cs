using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Equip : ScriptableObject, IWaitForSkill
{

    //protected List<Character> charactersThatCantUseMe = new List<Character>();
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
        //		user.changeEquipmentSprite(image);

        user.changeEquipObject(getBackEquip(), getFrontEquip());
        this.requester = requester;
        //charactersThatCantUseMe.Add(user);

        if (tile.isFromHero() == user.isPlayable())
        {
            if (tile.getIndex() == user.getPosition())
            {
                selfSkill.startEffect(user, tile, this);
            }
            else
            {
                alliesSkill.startEffect(user, tile, this);
            }
        }
        else
        {
            if ((Mathf.Abs(tile.getIndex() - user.getPosition()) <= meleeSkill.getRange()) && tile.getOccupant() != null)
            {
                meleeSkill.startEffect(user, tile, this);
            }
            else
            {
                rangedSkill.startEffect(user, tile, this);
            }
        }
    }

    public void resumeFromSkill()
    {
        requester.resumeFromEquipment();
    }

    public string getSkillName()
    {
        return eName;
    }

    public string getMySkillEffectsDescriptions()
    {
        string descriptions = "";
        descriptions += meleeSkill.getEffectName() + ": " + meleeSkill.getDescription() + "\n";
        descriptions += rangedSkill.getEffectName() + ": " + rangedSkill.getDescription() + "\n";
        descriptions += selfSkill.getEffectName() + ": " + selfSkill.getDescription() + "\n";
        descriptions += alliesSkill.getEffectName() + ": " + alliesSkill.getDescription() + "\n";
        return descriptions;
    }

    //public List<Character> getCharactersThatCantUseMe() { return charactersThatCantUseMe; }

    //public bool canCharaterUse(Character character)
    //{
    //    if (howManyTimesUsed(character) > character.howManyOf(this))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    //int howManyTimesUsed(Character character)
    //{
    //    int c = 0;
    //    foreach (Character cha in charactersThatCantUseMe)
    //    {
    //        if (character == cha)
    //        {
    //            c++;
    //        }
    //    }
    //    return c;
    //}

    public SkillEffect getMeleeEffect() { return meleeSkill; }
    public SkillEffect getSelfEffect() { return selfSkill; }
    public SkillEffect getRangedEffect() { return rangedSkill; }
    public SkillEffect getAlliesEffect() { return alliesSkill; }
    public List<SkillEffect> getAllSkillEffects()
    {
        List<SkillEffect> allSkillEffects = new List<SkillEffect>();
        if (meleeSkill != null)
            allSkillEffects.Add(meleeSkill);
        if (selfSkill != null)
            allSkillEffects.Add(selfSkill);
        if (rangedSkill != null)
            allSkillEffects.Add(rangedSkill);
        if (alliesSkill != null)
            allSkillEffects.Add(alliesSkill);
        return allSkillEffects;
    }
    public int getHp() { return hp; }
    public int getAtk() { return atk; }
    public int getAtkm() { return atkm; }
    public int getDef() { return def; }
    public int getDefm() { return defm; }

    public Image getBackEquip()
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
    public Image getFrontEquip()
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