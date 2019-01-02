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
            target.GetAttributes().GetHp().LoseHpBy((int)(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue()), false);
        }
        else
        {
            user.GetAttributes().GetMomentum().Value += user.IsPlayable() ? -Mathf.Abs(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue()) / 100 : Mathf.Abs(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue()) / 100;
            Debug.Log("Missed!");
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetAttributes().GetAtkmValue() - c1.GetAttributes().GetAtkmValue());

        return (int)(c1.GetAttributes().GetDefmValue() - c2.GetAttributes().GetDefmValue());
    }
}