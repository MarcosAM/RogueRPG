using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Wrath")]
public class Wrath : Damage
{
    protected override void PrepareDamage(Character user, Character target) { }

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().StartEffect(Attributes.Attribute.ATK, 2);
        target.GetAttributes().StartEffect(Attributes.Attribute.DEF, -2);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.Playable == c1.Playable)
            return c2.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKP) - c1.GetAttributes().GetSubAttribute(Attributes.SubAttribute.ATKP);
        else
            return c2.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP) - c1.GetAttributes().GetSubAttribute(Attributes.SubAttribute.DEFP);
    }

    public override string GetEffectDescription() { return ("Wrath"); }
}
