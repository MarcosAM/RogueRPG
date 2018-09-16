using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Revive")]
public class SERevive : Skill {

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);

		if (tile.getOccupant () != null) {
			if(!tile.getOccupant().isAlive()){
				OnHitEffect (user,tile);
				return;
			}
		}
		OnMissedEffect (user, tile);
	}

	public override void OnHitEffect (Character user, Battleground.Tile tile)
	{
		base.OnHitEffect (user, tile);
		tile.getOccupant ().revive (Mathf.RoundToInt(tile.getOccupant().getMaxHp() * value));
	}

	public override void OnMissedEffect (Character user, Battleground.Tile tile)
	{
		base.OnMissedEffect (user, tile);
	}
}
