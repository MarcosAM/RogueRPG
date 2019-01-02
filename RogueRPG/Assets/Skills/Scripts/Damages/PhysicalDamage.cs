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

    public override void TryToDamage(Character user, Character target, float attack)
    {
        base.TryToDamage(user, target, attack);
        if (Random.value < critic + user.GetAttributes().GetStatValue(Attribute.Stats.Critic))
        {
            target.GetAttributes().LoseHpBy((int)(user.GetAttributes().GetStatValue(Attribute.Stats.Atk) * criticIntensifier * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Def)), true);
            hitted = true;
        }
        else
        {
            if (hitted)
            {
                target.GetAttributes().LoseHpBy((int)(user.GetAttributes().GetStatValue(Attribute.Stats.Atk) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Def)), false);
            }
            else
            {
                user.GetAttributes().GetMomentum().Value += user.IsPlayable() ? -Mathf.Abs((user.GetAttributes().GetStatValue(Attribute.Stats.Atk) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Def))) / 100 : Mathf.Abs((user.GetAttributes().GetStatValue(Attribute.Stats.Atk) * Random.Range(1f, 1.2f) * dmgIntensifier - target.GetAttributes().GetStatValue(Attribute.Stats.Def))) / 100;
                Debug.Log("Missed!");
            }
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        if (user.GetAttributes().GetStatValue(Attribute.Stats.Critic) > 0.4f)
            return (int)(c2.GetAttributes().GetStatValue(Attribute.Stats.Def) - c1.GetAttributes().GetStatValue(Attribute.Stats.Def));

        if (user.GetInventory().Archetype >= Archetypes.Archetype.Brute)
            return CombatRules.GetDistance(user.GetTile(), c1.GetTile()) - CombatRules.GetDistance(user.GetTile(), c2.GetTile());

        if (user.GetInventory().Archetype == Archetypes.Archetype.Infantry)
            return (int)(c2.GetAttributes().GetStatValue(Attribute.Stats.Atk) - c1.GetAttributes().GetStatValue(Attribute.Stats.Atk));

        if (user.GetInventory().Archetype == Archetypes.Archetype.MInfantry)
            return (int)(c2.GetAttributes().GetStatValue(Attribute.Stats.Atkm) - c1.GetAttributes().GetStatValue(Attribute.Stats.Atkm));

        return (int)(c1.GetAttributes().GetStatValue(Attribute.Stats.Def) - c2.GetAttributes().GetStatValue(Attribute.Stats.Def));
    }
}