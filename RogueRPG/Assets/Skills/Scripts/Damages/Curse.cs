using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Damages/Curse")]
public class Curse : Damage
{
    protected override void PrepareDamage(Character user, Character target) { }

    protected override void OnHit(Character user, Character target)
    {
        if (target)
        {
            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                target.GetAttributes().StartEffect(attribute, -2);
            }
        }
    }

    int GetComparableValue(Character character)
    {
        int value = 6;

        if (character)
        {
            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                if (!character.GetAttributes().IsDebuffed(attribute))
                    value--;
            }
        }

        return value;
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }

    public override string GetEffectDescription() { return "Curse"; }
}