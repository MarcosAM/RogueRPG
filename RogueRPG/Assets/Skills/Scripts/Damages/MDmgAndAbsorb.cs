using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical and Absorb")]
public class MDmgAndAbsorb : MagicalDamage
{
    [Range(0.1f, 1f)]
    [SerializeField] float absorbRate;

    //TODO ver se eu consigo trasnformar tudo isso em Attributes
    protected override void OnHit(Character user, Character target)
    {
        base.OnHit(user, target);
        user.GetAttributes().Heal(Mathf.RoundToInt(damage * absorbRate));

        foreach (Attributes.Attribute attribute in Enum.GetValues(typeof(Attributes.Attribute)))
        {
            if (target.GetAttributes().GetEffectDuration(attribute) != 0)
            {
                user.GetAttributes().StartEffect(attribute, (int)(Mathf.Sign(target.GetAttributes().GetEffectDuration(attribute)) * 2));
            }
        }
    }
}