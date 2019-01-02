using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    Character character;

    List<Attribute> listStat = new List<Attribute>();

    Momentum momentum;

    public void Initialize(Character character)
    {
        this.character = character;
        this.momentum = FindObjectOfType<Momentum>();
        BuffPManager buffPManager = FindObjectOfType<BuffPManager>();
        foreach (Attribute.Type type in Enum.GetValues(typeof(Attribute.Type)))
        {
            if (type == Attribute.Type.Hp)
            {
                listStat.Add(new Hp(character, type, buffPManager, FindObjectOfType<Momentum>()));
                break;
            }
            listStat.Add(new Attribute(character, type, buffPManager));
        }
    }





    Attribute GetAttribute(Attribute.Type stats) { return listStat.Find(s => s.GetStats() == stats); }
    public float GetAttributeValue(Attribute.Type stats) { return GetAttribute(stats).GetValue(); }

    public float GetAtkValue() { return GetAttributeValue(Attribute.Type.Atk); }
    public float GetAtkmValue() { return GetAttributeValue(Attribute.Type.Atkm); }
    public float GetDefValue() { return GetAttributeValue(Attribute.Type.Def); }
    public float GetDefmValue() { return GetAttributeValue(Attribute.Type.Defm); }
    public float GetCriticValue() { return GetAttributeValue(Attribute.Type.Critic); }
    public float GetDodgeValue() { return GetAttributeValue(Attribute.Type.Dodge); }
    public float GetPrecisionValue() { return GetAttributeValue(Attribute.Type.Precision); }

    public void UpdateAttributes(Equip[] equips)
    {
        foreach (var equip in equips)
        {
            GetAttribute(Attribute.Type.Atk).AddToStatBase(equip.GetAtk());
            GetAttribute(Attribute.Type.Atkm).AddToStatBase(equip.GetAtkm());
            GetAttribute(Attribute.Type.Def).AddToStatBase(equip.GetDef());
            GetAttribute(Attribute.Type.Defm).AddToStatBase(equip.GetDefm());
            GetAttribute(Attribute.Type.Hp).AddToStatBase(equip.GetHp());
        }
    }

    public Hp GetHp() { return (Hp)GetAttribute(Attribute.Type.Hp); }


    public void Refresh()
    {
        RemoveAllBuffs();
        ((Hp)GetAttribute(Attribute.Type.Hp)).Refresh();
    }

    public void SpendBuffs()
    {
        foreach (Attribute stat in listStat)
        {
            stat.SpendAndCheckIfEnded();
        }
    }

    public void RemoveAllBuffs()
    {
        foreach (Attribute stat in listStat)
        {
            stat.ResetBuff();
        }
    }

    public void BuffIt(Attribute.Type stats, Attribute.Intensity intensity, int buffDuration)
    {
        listStat.Find(s => s.GetStats() == stats).BuffIt(intensity, buffDuration);
    }

    public bool IsBuffed(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetStats() == stats).GetBuffValue() > 0;
    }

    public float GetBuffValueOf(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetStats() == stats).GetBuffValue();
    }

    public Attribute.Intensity GetBuffIntensity(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetStats() == stats).GetIntensity();
    }

    public bool IsDebuffed()
    {
        return listStat.Exists(s => s.GetBuffValue() < 0);
    }

    public bool IsDebuffed(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetStats() == stats).GetBuffValue() < 0;
    }

    public Momentum GetMomentum() { return momentum; }
}
