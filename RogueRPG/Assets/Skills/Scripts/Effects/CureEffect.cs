using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Effects/Cure")]
public class CureEffect : Effects
{
    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);
        if (target)
        {
            target.GetAttributes().RemoveAllDebuffs();
        }
    }

    public override int GetComparableValue(Character character)
    {
        int value = 6;

        if (character)
        {
            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                if (character.GetAttributes().IsDebuffed(attribute))
                    value--;
            }
        }

        return value;
    }

    public override string GetEffectDescription()
    {
        return "Cure";
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        if (tile.GetCharacter())
        {
            return tile.GetCharacter().GetAttributes().IsDebuffed();
        }

        return false;
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }
}
