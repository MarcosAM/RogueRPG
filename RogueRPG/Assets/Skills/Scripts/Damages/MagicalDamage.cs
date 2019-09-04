﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical")]
public class MagicalDamage : Damage
{
    protected override void PrepareDamage(Character user, Character target)
    {
        damage = (int)(user.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKM) * Random.value + dmgIntensifier - target.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFM));
        Debug.Log(user.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKM));
    }

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().LoseHpBy(damage, false);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        //if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
        //    return (int)(c2.GetAttributes().GetAtkmValue() - c1.GetAttributes().GetAtkmValue());

        return (int)(c1.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFM) - c2.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFM));
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + " Magical damage."; }
}