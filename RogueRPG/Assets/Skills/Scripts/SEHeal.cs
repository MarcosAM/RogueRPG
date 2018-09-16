using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Heal")]
public class SEHeal : Skill {

//	public override void UniqueEffect (Character user, Skill skill, Battleground.Tile tile)
//	{
//		base.UniqueEffect (user, skill, tile);
//		if(tile.getOccupant() != null){
//			tile.getOccupant ().Heal (value+(int)user.getAtkmValue());
//		}
//	}

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
//		if (tile.getOccupant () != null)
		user.TryToHitWith (tile,this);
//			tile.getOccupant ().Heal (value + (int)user.getAtkmValue());
	}

	public override void OnHitEffect (Character user, Battleground.Tile tile)
	{
		base.OnHitEffect (user, tile);
		tile.getOccupant ().Heal (Mathf.RoundToInt(value + user.getAtkmValue()));
	}

	public override void OnMissedEffect (Character user, Battleground.Tile tile)
	{
		base.OnMissedEffect (user, tile);
	}
}