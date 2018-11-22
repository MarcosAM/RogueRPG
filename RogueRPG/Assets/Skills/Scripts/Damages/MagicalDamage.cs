using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical")]
public class MagicalDamage : Damage
{
    public override void TryToDamage(Character user, Character target, float attack)
    {
        base.TryToDamage(user, target, attack);
        if (hitted)
        {
            target.LoseHpBy((int)(user.GetStatValue(Stat.Stats.Atkm) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetStatValue(Stat.Stats.Defm)), false);
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}