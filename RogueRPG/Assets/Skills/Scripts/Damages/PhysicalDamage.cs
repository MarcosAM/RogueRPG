using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Physical")]
public class PhysicalDamage : Damage
{
    [SerializeField]
    [Range(0f, 1f)]
    protected float critic;
    public static float criticIntensifier = 1.5f;

    public override void TryToDamage(Character user, Character target, float attack)
    {
        base.TryToDamage(user, target, attack);
        if (Random.value < critic + user.GetStatValue(Stat.Stats.Critic))
        {
            target.LoseHpBy((int)(user.GetStatValue(Stat.Stats.Atk) * criticIntensifier * dmgIntensifier - target.GetStatValue(Stat.Stats.Def)), true);
            hitted = true;
        }
        else
        {
            if (hitted)
            {
                target.LoseHpBy((int)(user.GetStatValue(Stat.Stats.Atk) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetStatValue(Stat.Stats.Def)), false);
            }
            else
            {
                Debug.Log("Missed!");
            }
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.GetStatValue(Stat.Stats.Critic) > 0.4f)
            return (int)(c2.GetStatValue(Stat.Stats.Def) - c1.GetStatValue(Stat.Stats.Def));

        if (user.Archetype == Archetypes.Archetype.Infantry)
            return (int)(c2.GetStatValue(Stat.Stats.Atk) - c1.GetStatValue(Stat.Stats.Atk));

        if (user.Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetStatValue(Stat.Stats.Atkm) - c1.GetStatValue(Stat.Stats.Atkm));

        return (int)(c1.GetStatValue(Stat.Stats.Def) - c2.GetStatValue(Stat.Stats.Def));
    }
}