using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    Character character;

    int hp;
    int maxHp;
    bool alive = true;

    List<Attribute> listStat = new List<Attribute>();

    Momentum momentum;

    public event Action OnHUDValuesChange;
    public event Action<int, int, bool> OnHPValuesChange;

    public void Initialize(Character character)
    {
        this.character = character;
        this.momentum = FindObjectOfType<Momentum>();
        foreach (Attribute.Stats stat in Enum.GetValues(typeof(Attribute.Stats)))
        {
            listStat.Add(new Attribute(character, stat, FindObjectOfType<BuffPManager>()));
        }
    }





    public void LoseHpBy(int damage, bool wasCritic)
    {

        if (damage > 0)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(hp, damage, wasCritic);
            }

            hp -= damage;
            character.getAnimator().SetTrigger("Damage");

            if (hp <= 0)
            {
                Die();
            }
        }

        momentum.Value += character.IsPlayable() ? -(float)damage / 100 : (float)damage / 100;
        //TODO efeito de defender caso o dano seja menor que zero

        RefreshHUD();
    }

    public void Heal(int value)
    {
        if (value >= 0 && alive)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(hp, value, false);
            }
            hp += value;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
        RefreshHUD();
    }

    public void Die()
    {
        hp = 0;
        alive = false;
        EventManager.DeathOf(character);
        RemoveAllBuffs();
    }

    public void Revive(int hpRecovered)
    {
        alive = true;
        Heal(hpRecovered);
        DungeonManager.getInstance().AddToInitiative(character);
        RefreshHUD();
    }

    public bool IsAlive() { return alive; }

    public void AddToMaxHp(int value)
    {
        this.maxHp += value;
        this.hp = this.maxHp;
    }

    public void RefreshHUD()
    {
        if (OnHUDValuesChange != null)
        {
            OnHUDValuesChange();
        }
    }




    Attribute GetAttribute(Attribute.Stats stats) { return listStat.Find(s => s.GetStats() == stats); }
    public float GetAttributeValue(Attribute.Stats stats) { return GetAttribute(stats).GetValue(); }

    public float GetAtkValue() { return GetAttributeValue(Attribute.Stats.Atk); }
    public float GetAtkmValue() { return GetAttributeValue(Attribute.Stats.Atkm); }
    public float GetDefValue() { return GetAttributeValue(Attribute.Stats.Def); }
    public float GetDefmValue() { return GetAttributeValue(Attribute.Stats.Defm); }
    public float GetCriticValue() { return GetAttributeValue(Attribute.Stats.Critic); }
    public float GetDodgeValue() { return GetAttributeValue(Attribute.Stats.Dodge); }
    public float GetPrecisionValue() { return GetAttributeValue(Attribute.Stats.Precision); }

    public float GetHp() { return hp; }
    public float GetMaxHp() { return maxHp; }

    public void UpdateAttributes(Equip[] equips)
    {
        foreach (var equip in equips)
        {
            GetAttribute(Attribute.Stats.Atk).AddToStatBase(equip.GetAtk());
            GetAttribute(Attribute.Stats.Atkm).AddToStatBase(equip.GetAtkm());
            GetAttribute(Attribute.Stats.Def).AddToStatBase(equip.GetDef());
            GetAttribute(Attribute.Stats.Defm).AddToStatBase(equip.GetDefm());

            AddToMaxHp(equip.GetHp());
        }
    }




    public void Refresh()
    {
        RemoveAllBuffs();
        hp = maxHp;
    }

    public void SpendBuffs()
    {
        foreach (Attribute stat in listStat)
        {
            stat.SpendAndCheckIfEnded();
        }
    }

    void RemoveAllBuffs()
    {
        foreach (Attribute stat in listStat)
        {
            stat.ResetBuff();
        }
    }

    public void BuffIt(Attribute.Stats stats, Attribute.Intensity intensity, int buffDuration)
    {
        listStat.Find(s => s.GetStats() == stats).BuffIt(intensity, buffDuration);
    }

    public bool IsBuffed(Attribute.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).getBuffValue() > 0;
    }

    public float GetBuffValueOf(Attribute.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).getBuffValue();
    }

    public Attribute.Intensity GetBuffIntensity(Attribute.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).GetIntensity();
    }

    public bool IsDebuffed()
    {
        return listStat.Exists(s => s.getBuffValue() < 0);
    }

    public bool IsDebuffed(Attribute.Stats stats)
    {
        return listStat.Find(s => s.GetStats() == stats).getBuffValue() < 0;
    }

    public Momentum GetMomentum() { return momentum; }
}
