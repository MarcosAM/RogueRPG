﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipListItem : MonoBehaviour
{

    [SerializeField] Text amountTxt;
    [SerializeField] Text levelTxt;
    [SerializeField] Image archetypeIcon;
    [SerializeField] Text nameTxt;
    [SerializeField] Text attributesTxt;
    [SerializeField] SkillListItem[] skillsTxt;
    [SerializeField] Button mainBtn;
    [SerializeField] Image backgroundImg;

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
                skillsTxt[i].Initialize(skills[i].GetSkillName(), skills[i].GetDescription());
            else
                skillsTxt[i].Initialize("-");
        }

        if (equipped)
        {
            backgroundImg.color = Color.yellow;
            mainBtn.interactable = false;
        }
        else
        {
            mainBtn.interactable = true;
            backgroundImg.color = Color.grey;
        }
        archetypeIcon.sprite = Archetypes.GetArchetypeIcon(equip.GetArchetype());
        levelTxt.text = "Lv." + equip.GetLevel();
    }

    public void Fill(Archetypes.Archetype archetype)
    {
        amountTxt.text = none;
        mainBtn.interactable = false;
        nameTxt.text = unknown;
        attributesTxt.text = unknown;
        foreach (var skill in skillsTxt)
        {
            skill.Initialize(unknown);
        }
        backgroundImg.color = Color.grey;
        archetypeIcon.sprite = Archetypes.GetArchetypeIcon(archetype);
        levelTxt.text = "Lv.?";
    }

    public void BtnPress()
    {
        if (OnBtnPress != null)
        {
            OnBtnPress(equip);
        }
    }
}
