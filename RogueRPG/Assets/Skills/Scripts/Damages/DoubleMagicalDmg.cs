using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Double Magical Damage")]
public class DoubleMagicalDmg : MagicalDamage
{
    [SerializeField] SkillEffect otherEffect;

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().GetHp().LoseHpBy(damage, false);
        otherEffect.Affect(user, target);
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Magical damage and " + otherEffect.GetEffectDescription(); }
}