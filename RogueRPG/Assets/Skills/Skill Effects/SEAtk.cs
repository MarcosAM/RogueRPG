using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack")]
public class SEAtk : SkillEffect {

	public override void Effect (Skill skill, Battleground.Tile tile)
	{
		base.Effect (skill, tile);
		if (tile.getOccupant ())
			skill.getUser().Attack (tile.getOccupant(),value,skill);
	}
}