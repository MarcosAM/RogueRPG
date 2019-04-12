using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Damages/Remove")]
public class Remove : Damage
{
    public override string GetEffectDescription()
    {
        return "Remove";
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1.GetAttributes()) - GetComparableValue(c1.GetAttributes());
    }

    int GetComparableValue(Attributes attributes)
    {
        int value = 6;

        if (attributes)
        {
            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                if (attributes.IsBuffed(attribute))
                    return value--;
            }
        }

        return value;
    }

    protected override void OnHit(Character user, Character target)
    {
        if (target)
        {
            target.GetAttributes().RemoveAllBuffs();
        }
    }

    protected override void PrepareDamage(Character user, Character target) { }
}
