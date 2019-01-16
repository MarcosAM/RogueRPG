using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical")]
public class MagicalDamage : Damage
{
    public override void TryToAffect(Character user, Character target, float attack)
    {
        base.TryToAffect(user, target, attack);
        if (hitted)
        {
            target.GetAttributes().GetHp().LoseHpBy((int)(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue()), false);
        }
        else
        {
            user.GetAttributes().GetMomentum().Value += user.IsPlayable() ? -Mathf.Abs(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue()) / 100 : Mathf.Abs(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue()) / 100;
            target.GetAnimator().SetTrigger("Dodge");
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetAttributes().GetAtkmValue() - c1.GetAttributes().GetAtkmValue());

        return (int)(c1.GetAttributes().GetDefmValue() - c2.GetAttributes().GetDefmValue());
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Magical damage."; }
}