using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public enum Stats { Atk, Atkm, Def, Defm, Precision, Dodge, Critic };

    Character character;
    Stats stats;
    float statBase = 0;
    float buffValue = 0;
    int buffDuration = 0;
    List<float> bonus = new List<float>();
    IBuffHUD buffHUD;

    public Stat(Character character, Stats stats, IBuffHUD buffHUD)
    {
        this.character = character;
        this.stats = stats;
        this.buffHUD = buffHUD;
    }

    public float GetValue()
    {
        float value;
        value = statBase;
        value += buffValue;
        for (int i = 0; i < bonus.Count; i++)
        {
            value += bonus[i];
        }
        return value;
    }

    public void BuffIt(float buffValue, int buffDuration)
    {
        if (buffValue > this.buffValue && buffValue > 0)
        {
            this.buffValue = buffValue;
            this.buffDuration = buffDuration;
        }
        if (buffValue < this.buffValue && this.buffValue <= 0)
        {
            this.buffValue = buffValue;
            this.buffDuration = buffDuration;
        }
        if (buffValue == this.buffValue)
        {
            this.buffDuration = buffDuration;
        }
        buffHUD.PlayAt(stats,character.GetTile().getLocalPosition());
        //		TODO atualizar a interface para mostrar esse bonus
    }

    public void setStatBase(float value)
    {
        this.statBase = value;
    }
    public List<float> getBonus() { return bonus; }
    public float getBuffValue() { return buffValue; }
    public void ResetBuff()
    {
        buffValue = 0;
        buffDuration = 0;
        buffHUD.Stop();
    }
    public void SpendAndCheckIfEnded()
    {
        if (buffValue != 0)
        {
            buffDuration--;
            if (buffDuration <= 0)
            {
                ResetBuff();
            }
        }
    }

    public const float CRITIC_BUFF_1 = 0.1f;
    public const float CRITIC_BUFF_2 = 0.3f;
    public const float CRITIC_BUFF_3 = 0.5f;
    public const float CRITIC_DEBUFF_1 = -0.1f;
    public const float CRITIC_DEBUFF_2 = -0.3f;
    public const float CRITIC_DEBUFF_3 = -0.5f;
    public const float DODGE_BUFF_1 = 0.1f;
    public const float DODGE_BUFF_2 = 0.3f;
    public const float DODGE_BUFF_3 = 0.5f;
    public const float DODGE_DEBUFF_1 = -0.1f;
    public const float DODGE_DEBUFF_2 = -0.3f;
    public const float DODGE_DEBUFF_3 = -0.5f;
    public const float PRECISION_BUFF_1 = 0.1f;
    public const float PRECISION_BUFF_2 = 0.3f;
    public const float PRECISION_BUFF_3 = 0.5f;
    public const float PRECISION_DEBUFF_1 = -0.1f;
    public const float PRECISION_DEBUFF_2 = -0.3f;
    public const float PRECISION_DEBUFF_3 = -0.5f;
    public const float ATRIBUTE_BUFF_1 = 10;
    public const float ATRIBUTE_BUFF_2 = 20;
    public const float ATRIBUTE_BUFF_3 = 30;
    public const float ATRIBUTE_DEBUFF_1 = -10;
    public const float ATRIBUTE_DEBUFF_2 = -20;
    public const float ATRIBUTE_DEBUFF_3 = -30;
}