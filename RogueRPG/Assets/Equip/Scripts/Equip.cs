using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Equip : ScriptableObject, IWaitForSkill
{
    [SerializeField] protected string eName;

    [SerializeField] protected List<Skill> skills;
    [SerializeField] protected int hp, atk, atkm, def, defm;

    [Range(1, 10)]
    [SerializeField] protected int level;
    [SerializeField] protected Archetypes.Archetype archetype;

    [SerializeField] protected Image backEquipPrefab;
    [SerializeField] protected Image frontEquipPrefab;
    protected Image backEquip;
    protected Image frontEquip;

    protected IWaitForEquipment requester;

    public void UseEquipmentOn(Character user, Tile tile, IWaitForEquipment requester, int skill)
    {
        this.requester = requester;

        GetSkills()[skill].StartSkill(user, tile, this, user.GetInventory().IsMomentumEquip(this));
    }

    public void UseEquipment(Character user, IWaitForEquipment requester)
    {
        var turnSugestions = new List<TurnSugestion>();
        var probabilities = new List<int>();

        for (int i = 0; i < skills.Count; i++)
        {
            turnSugestions.Add(skills[i].GetTurnSugestion(user, Battleground.GetInstance()));
            for (int j = 0; j < turnSugestions[i].probability; j++)
            {
                probabilities.Add(i);
            }
        }

        //TODO Substituir pelo comentário abaixo
        var r = Random.Range(0, probabilities.Count);
        var index = probabilities[r];
        //TODO Substituir pelo comentário abaixo
        //var index = probabilities[Random.Range(0, probabilities.Count)];

        //TODO Deletar isso aqui depois
        var probabilitiesString = "As probabilidades são: ";
        foreach (int probability in probabilities)
        {
            probabilitiesString += probability + ", ";
        }
        Debug.Log(probabilitiesString + ". Mas eu escolhi: " + index + " graças a: " + r);
        //TODO Deletável ^^^^

        user.ChangeEquipObject(GetBackEquip(), GetFrontEquip());

        UseEquipmentOn(user, Battleground.GetInstance().GetTiles()[(int)turnSugestions[index].targetPosition], requester, index);
    }

    public void resumeFromSkill()
    {
        requester.ResumeFromEquipment();
    }

    public List<Skill> GetSkills() { return skills; }
    public string GetEquipName() { return eName; }
    public int GetHp() { return hp; }
    public int GetAtk() { return atk; }
    public int GetAtkm() { return atkm; }
    public int GetDef() { return def; }
    public int GetDefm() { return defm; }
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
    public Archetypes.Archetype GetArchetype() { return archetype; }
    public int GetLevel() { return level; }
}