using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Physical and Debuff")]
public class PDmgAndDebuff : PhysicalDamage
{
    [SerializeField] int duration;
    [SerializeField] Attributes.Attribute attribute;

    protected override void OnHit(Character user, Character target)
    {
        base.OnHit(user, target);
        target.GetAttributes().StartEffect(attribute, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return c2.GetAttributes().GetEffectDuration(attribute) - c1.GetAttributes().GetEffectDuration(attribute);
    }

    public override string GetEffectDescription() { return (dmgIntensifier + " Physical damage. Critic: " + critic * 100 + "%. " + ": "); }
}