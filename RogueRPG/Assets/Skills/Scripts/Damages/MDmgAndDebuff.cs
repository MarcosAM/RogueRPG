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
}