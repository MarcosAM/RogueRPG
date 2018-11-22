using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Revive")]
public class ReviveEffect : HealEffect
{
    [Range(0f, 1f)]
    [SerializeField]
    float precision;

    public override void Affect(Character user, Character target)
    {
        if (precision + user.GetStatValue(Stat.Stats.Precision) > Random.value)
        {
            target.Revive((int)(user.GetStatValue(Stat.Stats.Atkm) * healIntensifier));
        }
    }
}
