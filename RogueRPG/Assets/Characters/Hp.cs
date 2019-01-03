using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : Attribute
{
    int currentValue;
    bool alive = true;
    Momentum momentum;

    public event Action OnHUDValuesChange;
    public event Action<int, int, bool> OnHPValuesChange;

    public override float GetValue() { return currentValue; }

    public int GetMaxValue() { return (int)statBase; }

    public override void SpendAndCheckIfEnded()
    {
        if (intensity != Attribute.Intensity.None)
        {
            if (((int)this.intensity) % 2 == 0)
                Heal((int)(character.GetAttributes().GetHp().GetMaxValue() * GetBuffValue()));
            else
                LoseHpBy((int)(character.GetAttributes().GetHp().GetMaxValue() * GetBuffValue()), false);
        }
        base.SpendAndCheckIfEnded();
    }

    public override void AddToStatBase(float value)
    {
        base.AddToStatBase(value);
        this.currentValue = (int)statBase;
    }

    public void LoseHpBy(int damage, bool wasCritic)
    {

        if (damage > 0)
        {
            if (OnHPValuesChange != null)
            {
                OnHPValuesChange(currentValue, damage, wasCritic);
            }

            currentValue -= damage;
            character.GetAnimator().SetTrigger("Damage");

            if (currentValue <= 0)
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
                OnHPValuesChange(currentValue, value, false);
            }
            currentValue += value;
            if (currentValue > statBase)
            {
                currentValue = (int)statBase;
            }
        }
        RefreshHUD();
    }

    public void Die()
    {
        currentValue = 0;
        alive = false;
        EventManager.DeathOf(character);
        character.GetAttributes().RemoveAllBuffs();
    }

    public void Revive(int hpRecovered)
    {
        alive = true;
        Heal(hpRecovered);
        DungeonManager.getInstance().AddToInitiative(character);
        RefreshHUD();
    }

    public bool IsAlive() { return alive; }

    public void RefreshHUD()
    {
        if (OnHUDValuesChange != null)
        {
            OnHUDValuesChange();
        }
    }

    public void Refresh()
    {
        currentValue = (int)statBase;
    }

    public Hp(Character character, Type type, IBuffHUD buffHUD, Momentum momentum) : base(character, type, buffHUD)
    {
        this.momentum = momentum;
    }
}
