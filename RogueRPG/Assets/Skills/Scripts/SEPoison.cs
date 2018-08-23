using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Poison")]
public class SEPoison : SkillEffect {

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		user.TryToHitWith (tile,this);
	}

	public override void onHitEffect (Character user, Battleground.Tile tile)
	{
		base.onHitEffect (user, tile);
		tile.getOccupant ().getPoisoned();
	}

	public override void onMissedEffect (Character user, Battleground.Tile tile)
	{
		base.onMissedEffect (user, tile);
	}
}
