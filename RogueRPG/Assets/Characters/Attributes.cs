﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Attributes : MonoBehaviour
{
    Character character;

    public enum Attribute { HP = 0, ATK = 1, DEF = 2, AGI = 3 }
    public enum SubAttribute { ATKP = 0, ATKM = 1, DEFP = 2, DEFM = 3 }

    int hp, maxHp;
    bool alive = true;
    int[] subAttributes = new int[Enum.GetValues(typeof(SubAttribute)).Length];
    //WARNING: Sempre que for alterar esses valores, é necessário lembrar a characterHUD
    int[] effectsDurations = new int[Enum.GetValues(typeof(Attribute)).Length];

    public static readonly int atkAndDefBuff = 2;
    public static readonly int atkAndDefDebuff = -2;
    static readonly float agility = 0.2f;

    public event Action OnHUDValuesChange;
    public event Action<int, bool> OnHPValuesChange;
    public event Action<Attribute, int> OnEffectsChange;

    public void Initialize(Character character)
    {
        this.character = character;
        for (int i = 0; i < subAttributes.Length; i++)
        {
            subAttributes[i] = 0;
        }
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            effectsDurations[i] = 0;
        }
    }

    public void UpdateAttributes(Equip[] equips)
    {
        //TODO rever todo esse negócio de receber junto com os momentum equip
        for (int i = 0; i < equips.Length; i++)
        {
            SetMaxHP(maxHp + equips[i].GetHp());
            for (int l = 0; l < subAttributes.Length; l++)
            {
                subAttributes[l] += equips[i].GetSubAttribute((SubAttribute)l);
            }
        }
    }

    public int GetSubAttribute(SubAttribute subAttribute)
    {
        int modifier = 1;

        switch (subAttribute)
        {
            case SubAttribute.ATKP:
            case SubAttribute.ATKM:
                if (effectsDurations[(int)Attributes.Attribute.ATK] > 0)
                    modifier = atkAndDefBuff;
                if (effectsDurations[(int)Attributes.Attribute.ATK] < 0)
                    modifier = atkAndDefDebuff;
                break;
            default:
                if (effectsDurations[(int)Attributes.Attribute.DEF] > 0)
                    modifier = atkAndDefBuff;
                if (effectsDurations[(int)Attributes.Attribute.DEF] < 0)
                    modifier = atkAndDefDebuff;
                break;
        }

        return Mathf.RoundToInt(subAttributes[(int)subAttribute] + modifier);

    }

    public float GetAgility()
    {
        if (effectsDurations[(int)Attributes.Attribute.AGI] > 0)
            return agility;

        if (effectsDurations[(int)Attributes.Attribute.AGI] < 0)
            return -agility;

        return 0;
    }

    void SetMaxHP(int value)
    {
        maxHp = value;
        hp = maxHp;
    }

    public void Refresh()
    {
        RemoveAllEffects();
        alive = true;
        hp = maxHp;
    }

    public void CheckRegenerationAndPoison()
    {
        if (effectsDurations[(int)Attribute.HP] > 0)
        {
            Heal(maxHp / 4);
            return;
        }
        if (effectsDurations[(int)Attribute.HP] < 0)
        {
            LoseHpBy(maxHp / 4, false);
            return;
        }
    }

    public void SpendEffects()
    {
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            if (effectsDurations[i] > 0)
            {
                effectsDurations[i]--;
                if (effectsDurations[i] == 0)
                    EffectsChanged((Attribute)i, 0);
            }
            if (effectsDurations[i] < 0)
            {
                effectsDurations[i]++;
                if (effectsDurations[i] == 0)
                    EffectsChanged((Attribute)i, 0);
            }
        }
    }

    public void RemoveAllEffects()
    {
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            effectsDurations[i] = 0;
            EffectsChanged((Attribute)i, 0);
        }
    }

    public void StartEffect(Attribute attribute, int effectDuration)
    {
        if (alive)
        {
            int newDuration;

            if (effectsDurations[(int)attribute] != 0 && Mathf.Sign(effectsDurations[(int)attribute]) != Mathf.Sign(effectDuration))
            {
                newDuration = 0;
            }
            else
            {
                newDuration = effectDuration;
            }
            effectsDurations[(int)attribute] = newDuration;
            EffectsChanged(attribute, newDuration);
        }
    }

    public void RemoveAllBuffs()
    {
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            if (effectsDurations[i] > 0)
            {
                effectsDurations[i] = 0;
                EffectsChanged((Attribute)i, 0);
            }
        }
    }

    public void RemoveAllDebuffs()
    {
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            if (effectsDurations[i] < 0)
            {
                effectsDurations[i] = 0;
                EffectsChanged((Attribute)i, 0);
            }
        }
    }

    public int GetEffectDuration(Attribute attribute)
    {
        return effectsDurations[(int)attribute];
    }

    public bool IsBuffed(Attribute attribute)
    {
        return effectsDurations[(int)attribute] > 0;
    }

    public bool IsDebuffed()
    {
        return effectsDurations.Any(value => value < 0);
    }

    public bool IsDebuffed(Attribute attribute)
    {
        return effectsDurations[(int)attribute] < 0;
    }

    public void Heal(int value)
    {
        if (value >= 0 && alive)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(value, false);
            }
            hp += value;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
        RefreshHUD();
    }

    public void LoseHpBy(int damage, bool wasCritic)
    {

        if (damage > 0)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(-damage, wasCritic);
            }

            hp -= damage;
            character.GetAnimator().SetTrigger("Damage");

            if (hp <= 0)
            {
                Die();
            }
        }
        else
            character.GetAnimator().SetTrigger("Defend");

        RefreshHUD();
    }

    public void RefreshHUD()
    {
        if (OnHUDValuesChange != null)
        {
            OnHUDValuesChange();
        }
    }

    public void Die()
    {
        character.GetAnimator().SetTrigger("Death");
        hp = 0;
        alive = false;
        EventManager.DeathOf(character);
        RemoveAllEffects();
    }

    public void Revive(int hpRecovered)
    {
        character.GetAnimator().SetTrigger("Revive");
        alive = true;
        Heal(hpRecovered);
        DungeonManager.getInstance().AddToInitiative(character);
        RefreshHUD();
    }

    public bool IsAlive() { return alive; }
    public int GetHP() { return hp; }
    public int GetMaxHP() { return maxHp; }

    void EffectsChanged(Attribute attribute, int duration)
    {
        if (OnEffectsChange != null)
        {
            OnEffectsChange(attribute, duration);
        }
    }
}