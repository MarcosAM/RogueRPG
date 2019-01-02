using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical and Debuff")]
public class MDmgAndDebuff : MagicalDamage
{
    [SerializeField] int duration;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    public override void TryToDamage(Character user, Character target, float attack)
    {
        base.TryToDamage(user, target, attack);
        if (hitted)
            target.BuffIt(stat, intensity, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        switch (stat)
        {
            case Stat.Stats.Atk:
            case Stat.Stats.Atkm:
            case Stat.Stats.Def:
            case Stat.Stats.Defm:
                return (int)(c2.GetStatValue(stat) - c1.GetStatValue(stat));
            default:
                return c1.GetBuffIntensity(stat) - c2.GetBuffIntensity(stat);
        }
    }
}