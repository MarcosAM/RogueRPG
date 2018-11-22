using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Debuff")]
public class Debuff : Damage
{
    [SerializeField] int duration;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    public override void TryToDamage(Character user, Character target, float attack)
    {
        if (hitted)
        {
            target.BuffIt(stat, intensity, duration);
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}