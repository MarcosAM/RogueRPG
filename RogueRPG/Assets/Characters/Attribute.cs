using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute
{
    public enum Type { Hp, Atk, Atkm, Def, Defm, Precision, Dodge, Critic };
    public enum Intensity { None = 0, SmallDebuff = 1, SmallBuff = 2, MediumDebuff = 3, MediumBuff = 4, HighDebuff = 5, HighBuff = 6 };

    protected Character character;
    protected Type type;
    protected float statBase = 0;
    protected Intensity intensity = Intensity.None;
    protected int buffDuration = 0;
    protected IBuffHUD buffHUD;
    protected static List<List<float>> buffValues = new List<List<float>> { new List<float> { 1f, 0.8f, 1.5f, 0.5f, 1.8f, 0.3f, 2.5f }, new List<float> { 0, -0.1f, 0.1f, -0.3f, 0.3f, -0.5f, 0.5f } };

    public Attribute(Character character, Type stats, IBuffHUD buffHUD)
    {
        if (stats < Type.Precision)
            this.statBase = 1;
        this.character = character;
        this.type = stats;
        this.buffHUD = buffHUD;
    }

    public virtual float GetValue()
    {
        if (type < Type.Precision)
        {
            Debug.Log(character.Name + " " + type + " é " + (statBase * GetBuffValue()));
            return statBase * GetBuffValue();
        }

        Debug.Log(character.Name + " " + type + " é " + (statBase + GetBuffValue()));
        return statBase + GetBuffValue();
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
        buffHUD.PlayAt(character, type, this.intensity, character.GetTile().GetLocalPosition());
        //		TODO atualizar a interface para mostrar esse bonus
    }

    public virtual void AddToStatBase(float value) { statBase += value; }

    public float GetBuffValue()
    {
        switch (type)
        {
            case Type.Atk:
            case Type.Atkm:
            case Type.Def:
            case Type.Defm:
                return buffValues[0][(int)intensity];
            default:
                return buffValues[1][(int)intensity];
        }
    }
    public void ResetBuff()
    {
        intensity = Intensity.None;
        buffDuration = 0;
        buffHUD.Stop(character, type);
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

    public Type GetType() { return type; }

    void ChangeMomentumByUpping()
    {
        if (((int)this.intensity) % 2 == 0)
            character.GetAttributes().GetMomentum().Value += character.Playable ? (float)this.intensity / 100 : -(float)this.intensity / 100;
        else
            character.GetAttributes().GetMomentum().Value += character.Playable ? -(float)this.intensity / 100 : (float)this.intensity / 100;
    }
}