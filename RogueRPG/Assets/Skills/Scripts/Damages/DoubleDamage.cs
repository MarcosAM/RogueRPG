using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Double Damage")]
public class DoubleDamage : PhysicalDamage
{
    [SerializeField] SkillEffect otherEffect;

    protected override void OnHit(Character user, Character target)
    {
        target.GetAttributes().GetHp().LoseHpBy(damage, wasCritic);
        otherEffect.Affect(user, target);
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Physical damage. Critic: " + critic * 100 + "% and " + otherEffect.GetEffectDescription(); }
}
