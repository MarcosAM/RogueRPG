using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipListItem : MonoBehaviour
{

    [SerializeField] Text amountTxt;
    [SerializeField] Image archetypeIcon;
    [SerializeField] Text nameTxt;
    [SerializeField] Text attributesTxt;
    [SerializeField] Text[] skillsTxt;


    public void Fill(Equip equip)
    {
        nameTxt.text = equip.GetEquipName();

        attributesTxt.text = "";
        if (equip.GetHp() != 0)
            attributesTxt.text += "HP: " + equip.GetHp() + " ";
        if (equip.GetAtk() != 0)
            attributesTxt.text += "ATK: " + equip.GetAtk() + " ";
        if (equip.GetAtkm() != 0)
            attributesTxt.text += "ATKM: " + equip.GetAtkm() + " ";
        if (equip.GetDef() != 0)
            attributesTxt.text += "DEF: " + equip.GetDef() + " ";
        if (equip.GetDefm() != 0)
            attributesTxt.text += "DEFM: " + equip.GetDefm() + " ";

        var skills = equip.GetSkills();
        for (var i = 0; i < skillsTxt.Length; i++)
        {
            if (i < skills.Count)
                skillsTxt[i].text = skills[i].GetSkillName();
            else
                skillsTxt[i].text = "-";
        }
    }

}
