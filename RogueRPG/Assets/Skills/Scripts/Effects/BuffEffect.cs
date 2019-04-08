using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Buff")]
public class BuffEffect : Effects
{
    [SerializeField] int duration;
    [SerializeField] Attributes.Attribute attribute;

    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);
        if (target)
            target.GetAttributes().StartEffect(attribute, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        return tile.GetCharacter() ? !tile.GetCharacter().GetAttributes().IsBuffed(attribute) && tile.CharacterIs(true) : false;
    }

    public override int GetComparableValue(Character character)
    {
        if (character.GetAttributes().IsDebuffed(attribute))
        {
            return 1;
        }

        return 2;
    }

    public override string GetEffectDescription() { return ("Buff in " + attribute); }
}