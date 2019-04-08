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
    protected bool wasCritic = false;


    protected override void PrepareDamage(Character user, Character target)
    {
        if (Random.value < critic)
        {
            damage = (int)(user.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKP) * criticIntensifier * dmgIntensifier - target.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP));
            wasCritic = true;
            hitted = true;
        }
        else
        {
            damage = (int)(user.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKP) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP));
            wasCritic = false;
        }
    }

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().LoseHpBy(damage, wasCritic);
    }

    protected override void OnMiss(Character user, Character target)
    {
        //TODO Attributes não tem a menor necessidade de ter Momentum. Isso deveria ser uma static ou coisa parecida
        user.GetAttributes().GetMomentum().Value += user.Playable ? -damage / 100 : damage / 100;
        base.OnMiss(user, target);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return (int)(c1.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP) - c2.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP));
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Physical damage. Critic: " + critic * 100 + "%"; }
}