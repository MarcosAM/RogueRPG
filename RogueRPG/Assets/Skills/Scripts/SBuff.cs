using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Buff or Debuff")]
public class SBuff : Skill
{
    [SerializeField] int duration;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    protected override void UniqueEffect(Character user, Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().BuffIt(stat, intensity, duration);
        }
    }

    public Stat.Stats GetStats() { return stat; }
    public Stat.Intensity GetIntensity() { return intensity; }
}