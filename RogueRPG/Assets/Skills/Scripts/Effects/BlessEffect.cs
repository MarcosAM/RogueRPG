using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Effects/Bless")]
public class BlessEffect : Effects
{

    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);

        if (target)
        {
            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                target.GetAttributes().StartEffect(attribute, 3);
            }
        }
    }

    public override int GetComparableValue(Character character)
    {
        int value = 6;

        if (character)
        {
            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                if (!character.GetAttributes().IsBuffed(attribute))
                    value--;
            }
        }

        return value;
    }

    public override string GetEffectDescription()
    {
        return "Bless";
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        if (tile.GetCharacter())
        {
            Attributes attributes = tile.GetCharacter().GetAttributes();

            foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
            {
                if (!attributes.IsBuffed(attribute))
                    return true;
            }
        }

        return false;
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }
}
