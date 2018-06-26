using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack Magic")]
public class SEAtkm : SkillEffect {

	public override void Effect (Skill skill, Battleground.Tile tile)
	{
		base.Effect (skill, tile);
		if(tile.getOccupant() != null)
			skill.getUser ().AttackMagic (tile.getOccupant(),value,skill);
	}
}
