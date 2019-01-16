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

    public override void TryToAffect(Character user, Character target, float attack)
    {
        base.TryToAffect(user, target, attack);
        if (Random.value < critic + user.GetAttributes().GetCriticValue())
        {
            target.GetAttributes().GetHp().LoseHpBy((int)(user.GetAttributes().GetAtkValue() * criticIntensifier * dmgIntensifier - target.GetAttributes().GetDefValue()), true);
            hitted = true;
        }
        else
        {
            if (hitted)
            {
                target.GetAttributes().GetHp().LoseHpBy((int)(user.GetAttributes().GetAtkValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefValue()), false);
            }
            else
            {
                user.GetAttributes().GetMomentum().Value += user.IsPlayable() ? -Mathf.Abs((user.GetAttributes().GetAtkValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefValue())) / 100 : Mathf.Abs((user.GetAttributes().GetAtkValue() * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetDefValue())) / 100;
                target.GetAnimator().SetTrigger("Dodge");
            }
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.GetAttributes().GetCriticValue() > 0.4f)
            return (int)(c2.GetAttributes().GetDefValue() - c1.GetAttributes().GetDefValue());

        if (user.GetInventory().Archetype >= Archetypes.Archetype.Brute)
            return CombatRules.GetDistance(user.GetTile(), c1.GetTile()) - CombatRules.GetDistance(user.GetTile(), c2.GetTile());

        if (user.GetInventory().Archetype == Archetypes.Archetype.Infantry)
            return (int)(c2.GetAttributes().GetAtkValue() - c1.GetAttributes().GetAtkValue());

        if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetAttributes().GetAtkmValue() - c1.GetAttributes().GetAtkmValue());

        return (int)(c1.GetAttributes().GetDefValue() - c2.GetAttributes().GetDefValue());
    }

    public override string GetEffectDescription() { return dmgIntensifier * 100 + "% Physical damage. Critic: " + critic * 100 + "%"; }
}