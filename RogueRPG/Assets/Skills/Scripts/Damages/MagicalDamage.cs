using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical")]
public class MagicalDamage : Damage
{
    protected override void PrepareDamage(Character user, Character target)
    {
        damage = (int)(user.GetAttributes().GetAtkmValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefmValue());
    }

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().GetHp().LoseHpBy(damage, false);
    }

    protected override void OnMiss(Character user, Character target)
    {
        //TODO Attributes não tem a menor necessidade de ter Momentum. Isso deveria ser uma static ou coisa parecida
        user.GetAttributes().GetMomentum().Value += user.Playable ? -damage / 100 : damage / 100;
        base.OnMiss(user, target);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        //if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
        //    return (int)(c2.GetAttributes().GetAtkmValue() - c1.GetAttributes().GetAtkmValue());

        return (int)(c1.GetAttributes().GetDefmValue() - c2.GetAttributes().GetDefmValue());
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Magical damage."; }
}