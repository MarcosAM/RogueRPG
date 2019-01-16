using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical and Debuff")]
public class MDmgAndDebuff : MagicalDamage
{
    [SerializeField] int duration;
    [SerializeField] Attribute.Type type;
    [SerializeField] Attribute.Intensity intensity;

    public override void TryToAffect(Character user, Character target, float attack)
    {
        base.TryToAffect(user, target, attack);
        if (hitted)
            target.GetAttributes().BuffIt(type, intensity, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        switch (type)
        {
            case Attribute.Type.Atk:
            case Attribute.Type.Atkm:
            case Attribute.Type.Def:
            case Attribute.Type.Defm:
                return (int)(c2.GetAttributes().GetAttributeValue(type) - c1.GetAttributes().GetAttributeValue(type));
            default:
                return c1.GetAttributes().GetBuffIntensity(type) - c2.GetAttributes().GetBuffIntensity(type);
        }
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Magical damage. " + intensity + ": " + type; }

}