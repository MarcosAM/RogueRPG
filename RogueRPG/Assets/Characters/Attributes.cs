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
                continue;
            }
            listStat.Add(new Attribute(character, type, buffPManager));
        }
    }


    Attribute GetAttribute(Attribute.Type stats) { return listStat.Find(s => s.GetType() == stats); }
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
        for (var i = 0; i < equips.Length - 1; i++)
        {
            GetAttribute(Attribute.Type.Atk).AddToStatBase(equips[i].GetAtk());
            GetAttribute(Attribute.Type.Atkm).AddToStatBase(equips[i].GetAtkm());
            GetAttribute(Attribute.Type.Def).AddToStatBase(equips[i].GetDef());
            GetAttribute(Attribute.Type.Defm).AddToStatBase(equips[i].GetDefm());
            GetAttribute(Attribute.Type.Hp).AddToStatBase(equips[i].GetHp());
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
        if (GetHp().IsAlive())
            listStat.Find(s => s.GetType() == stats).BuffIt(intensity, buffDuration);
    }

    public bool IsBuffed(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetType() == stats).GetBuffValue() > 0;
    }

    public Attribute.Intensity GetBuffIntensity(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetType() == stats).GetIntensity();
    }

    public bool IsDebuffed()
    {
        return listStat.Exists(s => s.GetBuffValue() < 0);
    }

    public bool IsDebuffed(Attribute.Type stats)
    {
        return listStat.Find(s => s.GetType() == stats).GetBuffValue() < 0;
    }

    public void RemoveBuffOrDebuff(Attribute.Type type, bool buff)
    {
        if (buff)
        {
            if (IsBuffed(type))
            {
                GetAttribute(type).ResetBuff();
            }
        }
        else
        {
            if (IsDebuffed(type))
            {
                GetAttribute(type).ResetBuff();
            }
        }
    }

    public Momentum GetMomentum() { return momentum; }
    public Character GetCharacter() { return character; }
}
