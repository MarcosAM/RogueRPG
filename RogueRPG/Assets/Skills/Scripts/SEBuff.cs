using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Buff Debuff")]
public class SEBuff : SkillEffect {

	public enum BuffType {Atk, Atkm, Def, Defm, Precision, Dodge, Critic};
	[SerializeField]int duration;
	[SerializeField]BuffType buffType;

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		user.TryToHitWith (tile,this);
	}

	public override void onHitEffect (Character user, Battleground.Tile tile)
	{
		base.onHitEffect (user, tile);
		if (tile.getOccupant () != null) {
			switch (buffType) {
			case BuffType.Critic:
				switch (value) {
				case 1:
					tile.getOccupant ().CriticBuff (Stat.CRITIC_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().CriticBuff (Stat.CRITIC_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().CriticBuff (Stat.CRITIC_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().CriticBuff (Stat.CRITIC_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().CriticBuff (Stat.CRITIC_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().CriticBuff (Stat.CRITIC_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().CriticBuff (Stat.CRITIC_BUFF_1, duration);
					break;
				}
				break;
			case BuffType.Dodge:
				switch (value) {
				case 1:
					tile.getOccupant ().DodgeBuff (Stat.DODGE_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().DodgeBuff (Stat.DODGE_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().DodgeBuff (Stat.DODGE_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().DodgeBuff (Stat.DODGE_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().DodgeBuff (Stat.DODGE_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().DodgeBuff (Stat.DODGE_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().DodgeBuff (Stat.DODGE_BUFF_1, duration);
					break;
				}
				break;
			case BuffType.Precision:
				switch (value) {
				case 1:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_1, duration);
					break;
				}
				break;
			case BuffType.Atk:
				switch (value) {
				case 1:
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().AtkBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				}
				break;
			case BuffType.Atkm:
				switch (value) {
				case 1:
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().AtkmBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				}
				break;
			case BuffType.Def:
				switch (value) {
				case 1:
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().DefBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				}
				break;
			case BuffType.Defm:
				switch (value) {
				case 1:
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().DefmBuff (Stat.ATRIBUTE_BUFF_1, duration);
					break;
				}
				break;
			default:
				switch (value) {
				case 1:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_1, duration);
					break;
				case 2:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_2, duration);
					break;
				case 3: 
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_3, duration);
					break;
				case -1:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_DEBUFF_1, duration);
					break;
				case -2:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_DEBUFF_2, duration);
					break;
				case -3:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_DEBUFF_3, duration);
					break;
				default:
					tile.getOccupant ().PrecisionBuff (Stat.PRECISION_BUFF_1, duration);
					break;
				}
				break;
			}
		}
	}

	public override void onMissedEffect (Character user, Battleground.Tile tile)
	{
		base.onMissedEffect (user, tile);
	}
}