using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack Magic")]
public class SEAtkm : SkillEffect {

	public override void Effect (Character user, Skill skill, Battleground.Tile tile)
	{
		base.Effect (user, skill, tile);
		if(tile.getOccupant() != null)
			user.AttackMagic (tile.getOccupant(),skill.getValue(),skill);
	}
}
