using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Heal")]
public class SEHeal : SkillEffect {

	public override void Effect (Character user, Skill skill, Battleground.Tile tile)
	{
		base.Effect (user, skill, tile);
		if(tile.getOccupant() != null){
			tile.getOccupant ().Heal (skill.getValue()+(int)user.getAtkmValue());
		}
	}
}