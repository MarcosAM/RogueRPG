using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Magical and Absorb")]
public class MDmgAndAbsorb : MagicalDamage
{
    [Range(0.1f, 1f)]
    [SerializeField] float absorbRate;

    protected override void OnHit(Character user, Character target)
    {
        base.OnHit(user, target);
        user.GetAttributes().GetHp().Heal(Mathf.RoundToInt(damage * absorbRate));
    }
}
