using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack")]
public class SEAtk : SkillEffect {

	public override void Effect (Character user, Skill skill, Battleground.Tile tile)
	{
		base.Effect (user, skill, tile);
		if (tile.getOccupant ())
			user.Attack (tile.getOccupant(),value,skill);
	}
}