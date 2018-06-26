using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Move")]
public class SEMove : SkillEffect {

	public override void Effect (Skill skill, Battleground.Tile tile)
	{
		base.Effect (skill, tile);
		skill.getUser().getMovement ().MoveTo (tile.getPosition());
	}
}
