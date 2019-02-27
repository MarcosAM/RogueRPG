using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Buff")]
public class BuffEffect : Effects
{
    [SerializeField] int duration;
    [SerializeField] Attribute.Type type;
    [SerializeField] Attribute.Intensity intensity;

    public override void Affect(Character user, Character target)
    {
        base.Affect(user, target);
        if (target)
            target.GetAttributes().BuffIt(type, intensity, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        //TODO filosofar se eu deveria impedir de dar Regeneration para inimigos com full hp
        return tile.GetCharacter() ? tile.GetCharacter().GetAttributes().GetBuffIntensity(type) <= intensity && tile.CharacterIs(true) : false;
    }

    public override int GetComparableValue(Character character)
    {
        if (!character)
            return 6;
        Attribute.Intensity intensity = character.GetAttributes().GetBuffIntensity(this.type);
        if (intensity < this.intensity)
        {
            if (intensity == Attribute.Intensity.None)
                return 3;
            else
            {
                if ((int)intensity % 2 == 0)
                    return intensity == this.intensity ? 2 : 4;
                else
                    return 1;
            }
        }

        return 5;
    }

    public override string GetEffectDescription() { return (intensity + " in " + type); }
}