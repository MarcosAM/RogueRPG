using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Revive")]
public class SERevive : SkillEffect {

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);

		if (tile.getOccupant () != null) {
			if(!tile.getOccupant().isAlive()){
				onHitEffect (user,tile);
				return;
			}
		}
		onMissedEffect (user, tile);
	}

	public override void onHitEffect (Character user, Battleground.Tile tile)
	{
		base.onHitEffect (user, tile);
		tile.getOccupant ().revive (Mathf.RoundToInt(tile.getOccupant().getMaxHp() * value));
	}

	public override void onMissedEffect (Character user, Battleground.Tile tile)
	{
		base.onMissedEffect (user, tile);
	}
}
