using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Debuff")]
public class Debuff : Damage
{
    [SerializeField] protected int duration;
    [SerializeField] protected Attribute.Type type;
    [SerializeField] protected Attribute.Intensity intensity;

    public override void TryToAffect(Character user, Character target, float attack)
    {
        base.TryToAffect(user, target, attack);
        if (hitted)
        {
            target.GetAttributes().BuffIt(type, intensity, duration);
        }
        else
        {
            //TODO colocar esquiva aqui! A animação no caso, trigger the animation controller
            Debug.Log("Missed!" + attack);
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        //TODO ao menos estender a duração do debuff se o cara já tiver debuffado ou algo assim...
        return c1.GetAttributes().GetBuffIntensity(type) - c2.GetAttributes().GetBuffIntensity(type);
    }

    public override string GetEffectDescription() { return (intensity + " in " + type); }
}