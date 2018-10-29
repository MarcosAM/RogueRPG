using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public enum Stats { Atk, Atkm, Def, Defm, Precision, Dodge, Critic };
    public enum Intensity { None = 0, SmallDebuff = 1, SmallBuff = 2, MediumDebuff = 3, MediumBuff = 4, HighDebuff = 5, HighBuff = 6 };

    Character character;
    Stats stats;
    float statBase = 0;
    Intensity intensity = Intensity.None;
    int buffDuration = 0;
    List<float> bonus = new List<float>();
    IBuffHUD buffHUD;
    static List<List<float>> buffValues = new List<List<float>> { new List<float> { 0, -10f, 10f, -20f, 20f, -30f, 30f }, new List<float> { 0, -0.1f, 0.1f, -0.3f, 0.3f, -0.5f, 0.5f } };

    public Stat(Character character, Stats stats, IBuffHUD buffHUD)
    {
        this.character = character;
        this.stats = stats;
        this.buffHUD = buffHUD;
    }

    public float GetValue()
    {
        return statBase + getBuffValue();
    }

    public void BuffIt(Intensity intensity, int buffDuration)
    {
        if (intensity > this.intensity)
        {
            this.intensity = intensity;
            this.buffDuration = buffDuration;
        }
        else if (intensity == this.intensity && intensity < Intensity.HighDebuff)
        {
            this.intensity = this.intensity + 2;
            if (this.buffDuration < buffDuration)
                this.buffDuration = buffDuration;
        }
        buffHUD.PlayAt(character, stats, this.intensity, character.GetTile().getLocalPosition());
        //		TODO atualizar a interface para mostrar esse bonus
    }

    public void setStatBase(float value)
    {
        this.statBase = value;
    }
    public List<float> getBonus() { return bonus; }
    public float getBuffValue()
    {
        switch (stats)
        {
            case Stats.Atk:
            case Stats.Atkm:
            case Stats.Def:
            case Stats.Defm:
                return buffValues[0][(int)intensity];
            default:
                return buffValues[1][(int)intensity];
        }
    }
    public void ResetBuff()
    {
        intensity = Intensity.None;
        buffDuration = 0;
        buffHUD.Stop(character, stats);
    }
    public void SpendAndCheckIfEnded()
    {
        if (intensity != 0)
        {
            buffDuration--;
            if (buffDuration <= 0)
            {
                ResetBuff();
            }
        }
    }

    public Intensity GetIntensity() { return intensity; }

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