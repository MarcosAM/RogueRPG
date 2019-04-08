using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Buffs")]
public class Buffs : Effects
{
    //TODO criar Bless and Curse


    //[SerializeField] int duration;
    //[SerializeField] Attributes.Attribute[] attributes;

    //public override void Affect(Character user, Character target)
    //{
    //    base.Affect(user, target);

    //    if (target)
    //    {
    //        foreach (var type in attributes)
    //        {
    //            target.GetAttributes().StartEffect(type, duration);
    //        }
    //    }
    //}

    //public override int SortBestTargets(Character user, Character c1, Character c2)
    //{
    //    return GetComparableValue(c1) - GetComparableValue(c2);
    //}

    //public override bool IsLogicalTarget(Tile tile)
    //{
    //    if (!tile.GetCharacter())
    //        return false;

    //    if (!tile.CharacterIs(true))
    //        return false;

    //    Attributes targetAttributes = tile.GetCharacter().GetAttributes();

    //    foreach (var attribute in attributes)
    //    {
    //        if (duration > 0)
    //        {
    //            if (!target.GetAttributes().IsBuffed(attribute))
    //                return true;
    //        }
    //        else
    //        {
    //            if (!target.GetAttributes().IsDebuffed(attribute))
    //                return true;
    //        }
    //    }

    //    return false;
    //}

    //public override int GetComparableValue(Character character)
    //{
    //    if (!character)
    //        return 6;

    //    var value = 6;

    //    foreach (var type in attributes)
    //    {
    //        if (character.GetAttributes().GetBuffIntensity(type) <= intensity)
    //        {
    //            value--;
    //        }
    //    }

    //    return value;
    //}

    //public override string GetEffectDescription()
    //{
    //    var description = intensity + " in ";

    //    foreach (var type in attributes)
    //    {
    //        description += type + " ";
    //    }

    //    return description;
    //}
    public override int GetComparableValue(Character character)
    {
        throw new System.NotImplementedException();
    }

    public override string GetEffectDescription()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        throw new System.NotImplementedException();
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        throw new System.NotImplementedException();
    }
}
