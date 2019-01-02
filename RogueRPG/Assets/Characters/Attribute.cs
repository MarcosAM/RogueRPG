using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute
{
    public enum Stats { Atk, Atkm, Def, Defm, Precision, Dodge, Critic, Hp };
    public enum Intensity { None = 0, SmallDebuff = 1, SmallBuff = 2, MediumDebuff = 3, MediumBuff = 4, HighDebuff = 5, HighBuff = 6 };

    protected Character character;
    protected Stats stats;
    protected float statBase = 0;
    protected Intensity intensity = Intensity.None;
    protected int buffDuration = 0;
    protected List<float> bonus = new List<float>();
    protected IBuffHUD buffHUD;
    protected static List<List<float>> buffValues = new List<List<float>> { new List<float> { 0, -10f, 10f, -20f, 20f, -30f, 30f }, new List<float> { 0, -0.1f, 0.1f, -0.3f, 0.3f, -0.5f, 0.5f } };

    public Attribute(Character character, Stats stats, IBuffHUD buffHUD)
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
            ChangeMomentumByUpping();
        }
        buffHUD.PlayAt(character, stats, this.intensity, character.GetTile().GetLocalPosition());
        //		TODO atualizar a interface para mostrar esse bonus
    }

    //public void setStatBase(float value)
    //{
    //    this.statBase = value;
    //}

    public void AddToStatBase(float value) { statBase += value; }

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
    public virtual void SpendAndCheckIfEnded()
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

    public Stats GetStats() { return stats; }

    void ChangeMomentumByUpping()
    {
        if (((int)this.intensity) % 2 == 0)
            character.GetAttributes().GetMomentum().Value += character.IsPlayable() ? (float)this.intensity / 100 : -(float)this.intensity / 100;
        else
            character.GetAttributes().GetMomentum().Value += character.IsPlayable() ? -(float)this.intensity / 100 : (float)this.intensity / 100;
    }
}