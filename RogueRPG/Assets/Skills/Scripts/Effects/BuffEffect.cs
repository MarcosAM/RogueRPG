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
        return c1.GetBuffIntensity(stat) - c2.GetBuffIntensity(stat);
    }

    public override bool IsLogicalTarget(Tile tile)
    {
        //TODO filosofar se eu deveria impedir de dar Regeneration para inimigos com full hp
        return tile.GetCharacter() ? tile.GetCharacter().GetBuffIntensity(stat) < intensity : false;
    }
}