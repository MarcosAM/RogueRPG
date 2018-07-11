using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Defense Up")]
public class SEDefenseUp : SkillEffect {

	[SerializeField]int duration;

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		if (tile.getOccupant () != null) {
			switch (value) {
			case 1:
				tile.getOccupant().DefBuff (Stat.ATRIBUTE_BUFF_1,duration);
				break;
			case 2:
				tile.getOccupant().DefBuff (Stat.ATRIBUTE_BUFF_2,duration);
				break;
			case 3: 
				tile.getOccupant().DefBuff (Stat.ATRIBUTE_BUFF_3,duration);
				break;
			default:
				tile.getOccupant().DefBuff (Stat.ATRIBUTE_BUFF_1,duration);
				break;
			}
		}
	}
}
