using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Critic Up")]
public class SECriticUp : SkillEffect {

	[SerializeField]int duration;

	public override void Effect (Character user, Skill skill, Battleground.Tile tile)
	{
		base.Effect (user, skill, tile);
		if(tile.getOccupant() != null){
			switch(value){
			case 1:
				tile.getOccupant().CriticBuff (Stat.CRITIC_BUFF_1,duration);
				break;
			case 2:
				tile.getOccupant().CriticBuff (Stat.CRITIC_BUFF_2,duration);
				break;
			case 3: 
				tile.getOccupant().CriticBuff (Stat.CRITIC_BUFF_3,duration);
				break;
			default:
				tile.getOccupant().CriticBuff (Stat.CRITIC_BUFF_1,duration);
				break;
			}
		}
	}
}
