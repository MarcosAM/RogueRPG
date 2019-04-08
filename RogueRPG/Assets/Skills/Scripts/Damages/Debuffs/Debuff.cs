using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Debuff")]
public class Debuff : Damage
{
    //TODO deletar skills self se possível
    [SerializeField] protected int duration;
    [SerializeField] protected Attributes.Attribute attribute;

    protected override void PrepareDamage(Character user, Character target) { }

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().StartEffect(attribute, duration);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return c2.GetAttributes().GetEffectDuration(attribute) - c1.GetAttributes().GetEffectDuration(attribute);
    }

    public override string GetEffectDescription() { return (attribute + " Debuff"); }
}