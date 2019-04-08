using System;
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
    int[] effectsDurations = new int[Enum.GetValues(typeof(Attribute)).Length];

    float precisionDodge = 0f;

    Momentum momentum;

    public event Action OnHUDValuesChange;
    public event Action<int, bool> OnHPValuesChange;
    public event Action<Attribute, int> OnEffectsChange;

    public void Initialize(Character character)
    {
        this.character = character;
        this.momentum = FindObjectOfType<Momentum>();
        for (int i = 0; i < subAttributes.Length; i++)
        {
            subAttributes[i] = 0;
        }
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            effectsDurations[i] = 0;
        }
        //TODO linkar com o CharacterHUD para mostrar os icones de buffs e debuffs
    }

    public void UpdateAttributes(Equip[] equips)
    {
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
        //TODO Adicionar o efeitos dos buffs
        return subAttributes[(int)subAttribute];
    }
    public float GetAgility() { return precisionDodge; }

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
            Heal(maxHp / 5);
            return;
        }
        if (effectsDurations[(int)Attribute.HP] < 0)
        {
            LoseHpBy(maxHp / 5, false);
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
            }
            if (effectsDurations[i] < 0)
            {
                effectsDurations[i]++;
            }
        }
    }

    public void RemoveAllEffects()
    {
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            effectsDurations[i] = 0;
        }
    }

    public void StartEffect(Attribute attribute, int effectDuration)
    {
        if (effectsDurations[(int)attribute] != 0 && Mathf.Sign(effectsDurations[(int)attribute]) != Mathf.Sign(effectDuration))
        {
            effectsDurations[(int)attribute] = 0;
        }
        else
        {
            effectsDurations[(int)attribute] = effectDuration;
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

    public void RemoveAllBuffs()
    {
        for (int i = 0; i < effectsDurations.Length; i++)
        {
            if (effectsDurations[i] > 0)
            {
                effectsDurations[i] = 0;
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
            }
        }
    }

    //TODO ver uma maneira de me livrar disso. Provavelmente transformar momentum em static
    public Momentum GetMomentum() { return momentum; }
    //public Character GetCharacter() { return character; }

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

        momentum.Value += character.Playable ? -(float)damage / 100 : (float)damage / 100;

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
}