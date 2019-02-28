using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Physical and Absorb")]
public class PDmgAndAbsorb : PhysicalDamage
{
    [Range(0.1f, 1f)]
    [SerializeField] float absorbRate;

    protected override void OnHit(Character user, Character target)
    {
        base.OnHit(user, target);
        user.GetAttributes().GetHp().Heal(Mathf.RoundToInt(damage * absorbRate));

        foreach (Attribute.Type type in Enum.GetValues(typeof(Attribute.Type)))
        {
            if (target.GetAttributes().GetBuffIntensity(type) > user.GetAttributes().GetBuffIntensity(type))
            {
                user.GetAttributes().BuffIt(type, target.GetAttributes().GetBuffIntensity(type), 2);
            }
        }
    }
}
