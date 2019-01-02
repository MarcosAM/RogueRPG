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

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetStatValue(Stat.Stats.Atkm) - c1.GetStatValue(Stat.Stats.Atkm));

        return (int)(c1.GetStatValue(Stat.Stats.Defm) - c2.GetStatValue(Stat.Stats.Defm));
    }
}