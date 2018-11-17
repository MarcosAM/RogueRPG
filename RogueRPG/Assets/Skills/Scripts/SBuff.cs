using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Buff or Debuff")]
public class SBuff : Skill
{

    //public enum BuffType { Atk, Atkm, Def, Defm, Precision, Dodge, Critic };
    [SerializeField] int duration;
    //[SerializeField] BuffType buffType;
    [SerializeField] Stat.Stats stat;
    [SerializeField] Stat.Intensity intensity;

    public override void UniqueEffect(Character user, Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().BuffIt(stat, intensity, duration);
            //switch (stat)
            //{
            //    case Stat.Stats.Critic:
            //        tile.getOccupant().CriticBuff(intensity, duration);
            //        break;
            //    case Stat.Stats.Dodge:
            //        tile.getOccupant().DodgeBuff(intensity, duration);
            //        break;
            //    case Stat.Stats.Precision:
            //        tile.getOccupant().PrecisionBuff(intensity, duration);
            //        break;
            //    case Stat.Stats.Atk:
            //        tile.getOccupant().AtkBuff(intensity, duration);
            //        break;
            //    case Stat.Stats.Atkm:
            //        tile.getOccupant().AtkmBuff(intensity, duration);
            //        break;
            //    case Stat.Stats.Def:
            //        tile.getOccupant().DefBuff(intensity, duration);
            //        break;
            //    case Stat.Stats.Defm:
            //        tile.getOccupant().DefmBuff(intensity, duration);
            //        break;
            //    default:
            //        tile.getOccupant().PrecisionBuff(intensity, duration);
            //        break;
            //}
        }
    }

    public override void OnHitEffect(Character user, Tile tile)
    {
        base.OnHitEffect(user, tile);

    }

    public override void OnMissedEffect(Character user, Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }

    public Stat.Stats GetStats() { return stat; }
    public Stat.Intensity GetIntensity() { return intensity; }
}