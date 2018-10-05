using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Buff or Debuff")]
public class SBuff : Skill
{

    public enum BuffType { Atk, Atkm, Def, Defm, Precision, Dodge, Critic };
    [SerializeField] int duration;
    [SerializeField] BuffType buffType;
    [SerializeField] Stat.Intensity intensity;

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.getOccupant() != null)
        {
            switch (buffType)
            {
                case BuffType.Critic:
                    tile.getOccupant().CriticBuff(intensity, duration);
                    break;
                case BuffType.Dodge:
                    tile.getOccupant().DodgeBuff(intensity, duration);
                    break;
                case BuffType.Precision:
                    tile.getOccupant().PrecisionBuff(intensity, duration);
                    break;
                case BuffType.Atk:
                    tile.getOccupant().AtkBuff(intensity, duration);
                    break;
                case BuffType.Atkm:
                    tile.getOccupant().AtkmBuff(intensity, duration);
                    break;
                case BuffType.Def:
                    tile.getOccupant().DefBuff(intensity, duration);
                    break;
                case BuffType.Defm:
                    tile.getOccupant().DefmBuff(intensity, duration);
                    break;
                default:
                    tile.getOccupant().PrecisionBuff(intensity, duration);
                    break;
            }
        }
    }

    public override void OnHitEffect(Character user, Battleground.Tile tile)
    {
        base.OnHitEffect(user, tile);

    }

    public override void OnMissedEffect(Character user, Battleground.Tile tile)
    {
        base.OnMissedEffect(user, tile);
    }
}