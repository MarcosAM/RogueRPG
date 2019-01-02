using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damages/Debuff")]
public class Debuff : Damage
{
    [SerializeField] int duration;
    [SerializeField] Attribute.Type stat;
    [SerializeField] Attribute.Intensity intensity;

    public override void TryToDamage(Character user, Character target, float attack)
    {
        if (hitted)
        {
            target.GetAttributes().BuffIt(stat, intensity, duration);
        }
        else
        {
            Debug.Log("Missed!");
        }
    }

    public override int SortBestTargets(Character user, Character c1, Character c2)
    {
        //TODO ao menos estender a duração do debuff se o cara já tiver debuffado ou algo assim...
        return c1.GetAttributes().GetBuffIntensity(stat) - c2.GetAttributes().GetBuffIntensity(stat);
    }
}