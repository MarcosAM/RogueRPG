using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipListItem : MonoBehaviour
{
    [SerializeField] Text equippedTxt;
    [SerializeField] Image archetypeIcon;
    [SerializeField] Text nameTxt;
    [SerializeField] Text attributesTxt;
    [SerializeField] SkillListItem[] skillsTxt;
    [SerializeField] Button mainBtn;
    [SerializeField] Image backgroundImg;

    Equip equip;

    public event Action<Equip> OnBtnPress;

    static string UNKOWN = "???????";

    bool equipped;

    //public void Fill(Equip equip, bool equipped, int index)
    public void Fill(Equip equip, bool selected, bool equipped)
    {
        nameTxt.text = equip.GetEquipName();

        this.equip = equip;

        attributesTxt.text = "";
        if (equip.GetHp() != 0)
            attributesTxt.text += "HP: " + equip.GetHp() + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.ATKP) != 0)
            attributesTxt.text += "ATKP: " + equip.GetSubAttribute(Attributes.SubAttribute.ATKP) + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.ATKM) != 0)
            attributesTxt.text += "ATKM: " + equip.GetSubAttribute(Attributes.SubAttribute.ATKM) + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.DEFP) != 0)
            attributesTxt.text += "DEFP: " + equip.GetSubAttribute(Attributes.SubAttribute.DEFP) + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.DEFM) != 0)
            attributesTxt.text += "DEFM: " + equip.GetSubAttribute(Attributes.SubAttribute.DEFM) + " ";

        var skills = equip.GetSkills();
        for (var i = 0; i < skillsTxt.Length; i++)
        {
            if (i < skills.Count)
                skillsTxt[i].Initialize(skills[i].GetSkillName(), skills[i].GetDescription());
            else
                skillsTxt[i].Initialize("-");
        }

        if (selected)
        {
            backgroundImg.color = Color.grey;
            mainBtn.interactable = false;
        }
        else
        {
            mainBtn.interactable = true;
            backgroundImg.color = backgroundImg.color.lightGrey();
        }
        archetypeIcon.sprite = Archetypes.GetArchetypeIcon(equip.GetArchetype());

        this.equipped = equipped;
        equippedTxt.gameObject.SetActive(equipped);
    }

    public void BtnPress()
    {
        if (OnBtnPress != null && !equipped)
        {
            OnBtnPress(equip);
        }
    }
}
