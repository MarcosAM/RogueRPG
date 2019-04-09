using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootOption : MonoBehaviour
{
    [SerializeField] Text eName, eAttributes;
    [SerializeField] List<Text> eSkills;

    Equip equip;

    public void Refresh(Equip equip)
    {
        this.equip = equip;

        eName.text = equip.GetEquipName();

        eAttributes.text = "";
        if (equip.GetHp() != 0)
            eAttributes.text += "HP: " + equip.GetHp() + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.ATKP) != 0)
            eAttributes.text += "ATKP: " + equip.GetSubAttribute(Attributes.SubAttribute.ATKP) + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.ATKM) != 0)
            eAttributes.text += "ATKM: " + equip.GetSubAttribute(Attributes.SubAttribute.ATKM) + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.DEFP) != 0)
            eAttributes.text += "DEFP: " + equip.GetSubAttribute(Attributes.SubAttribute.DEFP) + " ";
        if (equip.GetSubAttribute(Attributes.SubAttribute.DEFM) != 0)
            eAttributes.text += "DEFM: " + equip.GetSubAttribute(Attributes.SubAttribute.DEFM) + " ";

        List<Skill> skills = equip.GetSkills();

        for (int i = 0; i < eSkills.Count; i++)
        {
            if (i < skills.Count)
            {
                eSkills[i].text = skills[i].GetSkillName();
            }
            else
            {
                eSkills[i].text = " - ";
            }
        }
    }

    public Equip GetEquip() { return equip; }
}