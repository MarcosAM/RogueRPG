using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Remove Buffs and Debuffs")]
public class RemoveBuffsOrDebuffs : Effects
{
    [SerializeField] private Attribute.Type[] types;
    [SerializeField] private readonly bool buff;

    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);
        if (target)
        {
            foreach (var type in types)
            {
                target.GetAttributes().RemoveBuffOrDebuff(type, buff);
            }
        }
    }

    public override int GetComparableValue(Character character)
    {
        if (!character)
        {
            return 6;
        }

        if (buff)
        {
            foreach (var type in types)
            {
                if (character.GetAttributes().IsBuffed(type))
                    return 2;
            }
        }
        else
        {
            foreach (var type in types)
            {
                if (character.GetAttributes().IsDebuffed(type))
                    return 2;
            }
        }

        return 6;
    }

    public override string GetEffectDescription()
    {
        var description = "Remove ";

        foreach (var type in types)
        {
            description += type + " ";
        }

        if (buff)
            description += "Buff(s)";
        else
            description += "Debuff(s)";

        return description;
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        if (!tile.GetCharacter())
            return false;

        if (buff)
        {
            foreach (var type in types)
            {
                if (tile.GetCharacter().GetAttributes().IsBuffed(type))
                    return true;
            }
        }
        else
        {
            foreach (var type in types)
            {
                if (tile.GetCharacter().GetAttributes().IsDebuffed(type))
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
