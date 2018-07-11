using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Move")]
public class SEMove : SkillEffect {

//	public override void UniqueEffect (Character user, Skill skill, Battleground.Tile tile)
//	{
//		base.UniqueEffect (user, skill, tile);
//		user.getMovement ().MoveTo (tile.getIndex());
//	}

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		user.getMovement ().MoveTo (tile.getIndex());
	}
}
