using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Buff")]
public class BuffEffect : Effects
{
    [SerializeField] int duration;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    public override void Affect(Character user, Character target)
    {
        target.BuffIt(stat, intensity, duration);
    }
    public Stat.Stats GetStats() { return stat; }
    public Stat.Intensity GetIntensity() { return intensity; }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return GetComparableValue(c1) - GetComparableValue(c2);
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        //TODO filosofar se eu deveria impedir de dar Regeneration para inimigos com full hp
        return tile.GetCharacter() ? tile.GetCharacter().GetBuffIntensity(stat) < intensity : false;
    }

    public override int GetComparableValue(Character character)
    {
        if (!character)
            return 6;
        Stat.Intensity intensity = character.GetBuffIntensity(this.stat);
        if (intensity < this.intensity)
        {
            if (intensity == Stat.Intensity.None)
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
}