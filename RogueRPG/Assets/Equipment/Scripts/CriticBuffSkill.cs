using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Buffs/Critic")]
public class CriticBuffSkill : Skill {

	[SerializeField]int bDuration;
	
//	public override void UniqueEffect (Character user, Character target){
//		base.UniqueEffect (user, target);
//		switch(sValue){
//		case 1:
//			target.CriticBuff (Stat.CRITIC_BUFF_1,bDuration);
//			break;
//		case 2:
//			target.CriticBuff (Stat.CRITIC_BUFF_2,bDuration);
//			break;
//		case 3: 
//			target.CriticBuff (Stat.CRITIC_BUFF_3,bDuration);
//			break;
//		default:
//			target.CriticBuff (Stat.CRITIC_BUFF_1,bDuration);
//			break;
//		}
//	}
}