using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Buff")]
public class BuffEffect : Effect
{
    [SerializeField] int duration;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    public override void Start(Character user, Character target)
    {
        target.BuffIt(stat, intensity, duration);
    }
    public Stat.Stats GetStats() { return stat; }
    public Stat.Intensity GetIntensity() { return intensity; }
}
