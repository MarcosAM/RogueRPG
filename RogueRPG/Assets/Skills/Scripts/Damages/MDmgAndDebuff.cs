﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical and Debuff")]
public class MDmgAndDebuff : MagicalDamage
{
    [SerializeField] int duration;
    [SerializeField] Attribute.Type stat;
    [SerializeField] Attribute.Intensity intensity;

    public override void TryToDamage(Character user, Character target, float attack)
    {
        base.TryToDamage(user, target, attack);
        if (hitted)
            target.GetAttributes().BuffIt(stat, intensity, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        switch (stat)
        {
            case Attribute.Type.Atk:
            case Attribute.Type.Atkm:
            case Attribute.Type.Def:
            case Attribute.Type.Defm:
                return (int)(c2.GetAttributes().GetAttributeValue(stat) - c1.GetAttributes().GetAttributeValue(stat));
            default:
                return c1.GetAttributes().GetBuffIntensity(stat) - c2.GetAttributes().GetBuffIntensity(stat);
        }
    }
}