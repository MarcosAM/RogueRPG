﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Buff or Debuff")]
public class SBuff : Skill
{

    public enum BuffType { Atk, Atkm, Def, Defm, Precision, Dodge, Critic };
    [SerializeField] int duration;
    [SerializeField] BuffType buffType;

    public override void UniqueEffect(Character user, Battleground.Tile tile)
    {
        base.UniqueEffect(user, tile);
        if (tile.getOccupant() != null)
        {
            switch (buffType)
            {
                case BuffType.Critic:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().CriticBuff(Stat.CRITIC_BUFF_1, duration);
                            break;
                    }
                    break;
                case BuffType.Dodge:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().DodgeBuff(Stat.DODGE_BUFF_1, duration);
                            break;
                    }
                    break;
                case BuffType.Precision:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_1, duration);
                            break;
                    }
                    break;
                case BuffType.Atk:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().AtkBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                    }
                    break;
                case BuffType.Atkm:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().AtkmBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                    }
                    break;
                case BuffType.Def:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().DefBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                    }
                    break;
                case BuffType.Defm:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().DefmBuff(Stat.ATRIBUTE_BUFF_1, duration);
                            break;
                    }
                    break;
                default:
                    switch (Mathf.RoundToInt(value))
                    {
                        case 1:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_1, duration);
                            break;
                        case 2:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_2, duration);
                            break;
                        case 3:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_3, duration);
                            break;
                        case -1:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_DEBUFF_1, duration);
                            break;
                        case -2:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_DEBUFF_2, duration);
                            break;
                        case -3:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_DEBUFF_3, duration);
                            break;
                        default:
                            tile.getOccupant().PrecisionBuff(Stat.PRECISION_BUFF_1, duration);
                            break;
                    }
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