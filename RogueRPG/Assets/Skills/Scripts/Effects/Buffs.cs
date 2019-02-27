using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Buffs")]
public class Buffs : Effects
{

    [SerializeField] int duration;
    [SerializeField] Attribute.Type[] types;
    [SerializeField] Attribute.Intensity intensity;

    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);

        if (target)
        {
            foreach (var type in types)
            {
                target.GetAttributes().BuffIt(type, intensity, duration);
            }
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        if (!tile.GetCharacter())
            return false;

        if (!tile.CharacterIs(true))
            return false;

        foreach (var type in types)
        {
            if (tile.GetCharacter().GetAttributes().GetBuffIntensity(type) <= intensity)
                return true;
        }

        return false;
    }

    public override int GetComparableValue(Character character)
    {
        if (!character)
            return 6;

        var value = 6;

        foreach (var type in types)
        {
            if (character.GetAttributes().GetBuffIntensity(type) <= intensity)
            {
                value--;
            }
        }

        return value;
    }

    public override string GetEffectDescription()
    {
        var description = intensity + " in ";

        foreach (var type in types)
        {
            description += type + " ";
        }

        return description;
    }
}
