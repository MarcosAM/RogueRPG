using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Move")]
public class SEMove : SkillEffect {

	public override void Effect (Character user, Skill skill, Battleground.Tile tile)
	{
		base.Effect (user, skill, tile);
		Debug.Log ("Move meu fi");
		user.getMovement ().MoveTo (tile.getIndex());
	}
}
