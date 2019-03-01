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
                dmgIntensifier = .8f;
                break;
            case 1:
                dmgIntensifier = .9f;
                break;
            case 2:
                dmgIntensifier = 1f;
                break;
            case 3:
                dmgIntensifier = 1.2f;
                break;
            case 4:
                dmgIntensifier = 1.5f;
                break;
            case 5:
                dmgIntensifier = 2f;
                break;
        }

        base.PrepareDamage(user, target);
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        return CombatRules.GetDistance(user.GetTile(), c2.GetTile()) - CombatRules.GetDistance(user.GetTile(), c1.GetTile());
    }
}