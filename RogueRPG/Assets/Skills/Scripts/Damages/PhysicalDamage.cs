using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Physical")]
public class PhysicalDamage : Damage
{
    [SerializeField]
    [Range(0f, 1f)]
    protected float critic;
    protected bool wasCritic = false;


    protected override void PrepareDamage(Character user, Character target)
    {
        if (Random.value < critic)
        {
            damage = (int)(user.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKP) + dmgIntensifier);
            wasCritic = true;
            hitted = true;
        }
        else
        {
            damage = (int)(user.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKP) * Random.value + dmgIntensifier - target.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP));
            wasCritic = false;
        }
    }

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().LoseHpBy(damage, wasCritic);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return (int)(c1.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP) - c2.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP));
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + " Physical damage. Critic: " + critic * 100 + "%"; }
}