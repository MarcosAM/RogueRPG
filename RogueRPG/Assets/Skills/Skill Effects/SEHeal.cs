using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Heal")]
public class SEHeal : SkillEffect {

	public override void Effect (Skill skill, Battleground.Tile tile)
	{
		base.Effect (skill, tile);
		if(tile.getOccupant() != null){
			tile.getOccupant ().Heal (value+(int)skill.getUser().getAtkmValue());
		}
	}
}