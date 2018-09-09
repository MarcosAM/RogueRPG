using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack Magic")]
public class SEAtkm : SkillEffect {

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

	public override void onHitEffect (Character user, Battleground.Tile tile)
	{
		base.onHitEffect (user, tile);
		user.HitWith (tile.getOccupant(),value,this);
	}

	public override void onMissedEffect (Character user, Battleground.Tile tile)
	{
		base.onMissedEffect (user, tile);
	}
}
