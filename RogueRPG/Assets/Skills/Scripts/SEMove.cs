using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Move")]
public class SEMove : Skill {

//	public override void UniqueEffect (Character user, Skill skill, Battleground.Tile tile)
//	{
//		base.UniqueEffect (user, skill, tile);
//		user.getMovement ().MoveTo (tile.getIndex());
//	}

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		OnHitEffect (user, tile);
	}

	public override void OnHitEffect (Character user, Battleground.Tile tile)
	{
		base.OnHitEffect (user, tile);
		user.getMovement ().MoveTo (tile.getIndex());
	}

	public override void OnMissedEffect (Character user, Battleground.Tile tile)
	{
		base.OnMissedEffect (user, tile);
	}
}
