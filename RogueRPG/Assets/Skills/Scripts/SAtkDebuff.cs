using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Attack and Debuff")]
public class SAtkDebuff : SAtk
{
    [SerializeField] int duration;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    protected override void UniqueEffect(Character user, Tile tile)
    {
        if (tile.GetCharacter())
        {
            if (WasCritic())
            {
                damages.Add(Damage(tile.GetCharacter(), dmg, true));
                tile.GetCharacter().BuffIt(stat, intensity, duration);
            }
            else
            {
                if (DidIHit(tile.GetCharacter(), hit))
                {
                    damages.Add(Damage(tile.GetCharacter(), dmg, false));
                    tile.GetCharacter().BuffIt(stat, intensity, duration);
                    Debug.Log("Hit!");
                }
                else
                {
                    damages.Add(0);
                    Debug.Log("Missed!");
                }
            }
        }
    }
}
