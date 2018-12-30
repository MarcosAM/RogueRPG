using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Equip : ScriptableObject, IWaitForSkill
{
    [SerializeField] protected string eName;

    [SerializeField] protected List<Skill> skills;
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
        GetSkills()[skill].StartSkill(user, tile, this, user.IsMomentumEquip(this));
    }

    public void UseEquipment(Character user, IWaitForEquipment requester, bool momentum)
    {
        var skills = GetSkills();
        var turnSugestions = new List<TurnSugestion>();
        var probabilities = new List<int>();
        var battleground = FindObjectOfType<Battleground>();

        for (int i = 0; i < skills.Count; i++)
        {
            turnSugestions.Add(skills[i].GetTurnSugestion(user, battleground));
            for (int j = 0; j < turnSugestions[i].probability; j++)
            {
                probabilities.Add(i);
            }
        }

        var index = probabilities[Random.Range(0, probabilities.Count)];
        var allTiles = battleground.GetTiles();

        UseEquipmentOn(user, allTiles[(int)turnSugestions[index].targetPosition], requester, momentum, index);
    }

    public void resumeFromSkill()
    {
        requester.ResumeFromEquipment();
    }

    public string GetEquipName() { return eName; }
    public int GetHp() { return hp; }
    public int GetAtk() { return atk; }
    public int GetAtkm() { return atkm; }
    public int GetDef() { return def; }
    public int GetDefm() { return defm; }
    public List<Skill> GetSkills()
    {
        return skills;
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