using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack Magic")]
public class SEAtkm : Skill {

//	public override void UniqueEffect (Character user, Skill skill, Battleground.Tile tile)
//	{
//		base.UniqueEffect (user, skill, tile);
//		if(tile.getOccupant() != null)
//			user.AttackMagic (tile.getOccupant(),value,skill);
//	}

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		if (tile.getOccupant () != null)
			user.TryToHitWith (tile,this);
	}

	public override void OnHitEffect (Character user, Battleground.Tile tile)
	{
		base.OnHitEffect (user, tile);
		user.HitWith (tile.getOccupant(),value,this);
	}

	public override void OnMissedEffect (Character user, Battleground.Tile tile)
	{
		base.OnMissedEffect (user, tile);
	}
}
