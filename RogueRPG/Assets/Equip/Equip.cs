﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Equip")]
public class Equip : ScriptableObject, IWaitForSkill
{
    //TODO make the own equip paint the sprite
    [SerializeField] protected string eName;

    [SerializeField] protected List<Skill> skills;
    [SerializeField] protected int hp;
    [SerializeField] protected int[] subAttributes;

    [SerializeField] protected Archetypes.Archetype archetype;

    [SerializeField] protected RectTransform backEquipPrefab;
    [SerializeField] protected RectTransform frontEquipPrefab;

    protected IWaitForEquipment requester;

    public void UseEquipmentOn(Character user, Tile tile, IWaitForEquipment requester, int skill)
    {
        this.requester = requester;
        SkillName.ShowSkillName(GetSkills()[skill].GetSkillName());
        GetSkills()[skill].StartSkill(user, tile, this);
    }

    public void UseEquipment(Character user, IWaitForEquipment requester, int equipIndex)
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

        user.ChangeEquipObject(equipIndex);

        if (probabilities.Count > 0)
        {
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

            UseEquipmentOn(user, Battleground.GetInstance().GetTiles()[(int)turnSugestions[index].targetPosition], requester, index);
        }
        else
        {
            //TODO se livrar desse requester, pq sempre vai retornar par ao turn manager
            SkillName.ShowSkillName("Pass");
            this.requester = requester;
            resumeFromSkill();
        }
    }

    public void resumeFromSkill()
    {
        requester.ResumeFromEquipment();
    }

    public List<Skill> GetSkills() { return skills; }
    public string GetEquipName() { return eName; }
    public int GetHp() { return hp; }
    public int GetSubAttribute(Attributes.SubAttribute subAttribute) { return subAttributes[(int)subAttribute]; }
    public RectTransform GetBackEquipPrefab() { return backEquipPrefab; }
    public RectTransform GetFrontEquipPrefab() { return frontEquipPrefab; }
    public Archetypes.Archetype GetArchetype() { return archetype; }
}