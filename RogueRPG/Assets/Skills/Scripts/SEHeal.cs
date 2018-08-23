using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Heal")]
public class SEHeal : SkillEffect {

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

	public override void onHitEffect (Character user, Battleground.Tile tile)
	{
		base.onHitEffect (user, tile);
		tile.getOccupant ().Heal (Mathf.RoundToInt(value + user.getAtkmValue()));
	}

	public override void onMissedEffect (Character user, Battleground.Tile tile)
	{
		base.onMissedEffect (user, tile);
	}
}