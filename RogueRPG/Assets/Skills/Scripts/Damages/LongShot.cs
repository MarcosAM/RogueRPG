using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Long Physical Shot")]
public class LongShot : PhysicalDamage
{
    protected override void PrepareDamage(Character user, Character target)
    {
        var distance = CombatRules.GetDistance(user.GetTile(), target.GetTile());

        switch (distance)
        {
            case 0:
                dmgIntensifier = 0;
                break;
            case 1:
                dmgIntensifier = 0;
                break;
            case 2:
                dmgIntensifier = 3;
                break;
            case 3:
                dmgIntensifier = 5;
                break;
            case 4:
                dmgIntensifier = 7;
                break;
            case 5:
                dmgIntensifier = 7;
                break;
        }

        base.PrepareDamage(user, target);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return CombatRules.GetDistance(user.GetTile(), c2.GetTile()) - CombatRules.GetDistance(user.GetTile(), c1.GetTile());
    }

    public override string GetEffectDescription() { return "0~7 Physical damage. Critic: " + critic * 100 + "%"; }
}