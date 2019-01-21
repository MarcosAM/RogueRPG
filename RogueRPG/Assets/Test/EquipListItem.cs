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
    [SerializeField] Button mainBtn;

    Equip equip;

    public event Action<Equip> OnBtnPress;

    static string unknown = "???????";
    static string none = "0";

    public void Fill(Equip equip, bool equipped)
    {
        amountTxt.text = equip.GetHowManyLeft() + "/" + equip.GetAmount();

        nameTxt.text = equip.GetEquipName();

        this.equip = equip;

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

        if (equipped)
        {
            archetypeIcon.color = Color.yellow;
            mainBtn.interactable = false;
        }
        else
        {
            mainBtn.interactable = true;
            archetypeIcon.color = Color.white;
        }
    }

    public void Fill(Archetypes.Archetype archetype)
    {
        amountTxt.text = none;
        mainBtn.interactable = false;
        nameTxt.text = unknown;
        attributesTxt.text = unknown;
        foreach (var skill in skillsTxt)
        {
            skill.text = unknown;
        }
        archetypeIcon.color = Color.white;
    }

    public void BtnPress()
    {
        if (OnBtnPress != null)
        {
            OnBtnPress(equip);
        }
    }
}
