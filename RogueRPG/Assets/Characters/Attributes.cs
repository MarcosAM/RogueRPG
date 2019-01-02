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

    public void RefreshHUD()
    {
        if (OnHUDValuesChange != null)
        {
            OnHUDValuesChange();
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

    public float GetHp() { return hp; }
    public float GetMaxHp() { return maxHp; }

    public float GetStatValue(Attribute.Stats stats)
    {
        if (listStat.Exists(s => s.GetStats() == stats))
        {
            return listStat.Find(s => s.GetStats() == stats).GetValue();
        }
        else
        {
            return 0;
        }
    }

    public Attribute GetStat(Attribute.Stats stats)
    {
        if (listStat.Exists(s => s.GetStats() == stats))
        {
            return listStat.Find(s => s.GetStats() == stats);
        }
        else
        {
            return null;
        }
    }

    public bool IsAlive() { return alive; }

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

    public void AddToMaxHp(int value)
    {
        this.maxHp += value;
        this.hp = this.maxHp;
    }
}
