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
            target.GetAttributes().LoseHpBy((int)(user.GetAttributes().GetStatValue(Attribute.Stats.Atkm) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Defm)), false);
        }
        else
        {
            user.GetAttributes().GetMomentum().Value += user.IsPlayable() ? -Mathf.Abs(user.GetAttributes().GetStatValue(Attribute.Stats.Atkm) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Defm)) / 100 : Mathf.Abs(user.GetAttributes().GetStatValue(Attribute.Stats.Atkm) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Defm)) / 100;
            Debug.Log("Missed!");
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetAttributes().GetStatValue(Attribute.Stats.Atkm) - c1.GetAttributes().GetStatValue(Attribute.Stats.Atkm));

        return (int)(c1.GetAttributes().GetStatValue(Attribute.Stats.Defm) - c2.GetAttributes().GetStatValue(Attribute.Stats.Defm));
    }
}