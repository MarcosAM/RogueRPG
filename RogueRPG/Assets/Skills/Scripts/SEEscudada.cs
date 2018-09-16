using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Escudada")]
public class SEEscudada : Skill {

	[SerializeField]int buffValue;
	float defValue;
	[SerializeField]int duration;

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
//		if (tile.getOccupant ())
			user.TryToHitWith (tile,this);
	}

	public override void OnHitEffect (Character user, Battleground.Tile tile)
	{
		base.OnHitEffect (user, tile);
		user.HitWith (tile.getOccupant (), value, this);
		Battleground.Tile[] heroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
		if(buffValue == 1){
			defValue = Stat.ATRIBUTE_BUFF_1;
		}
		if(buffValue == 2){
			defValue = Stat.ATRIBUTE_BUFF_2;
		}
		if(buffValue == 3){
			defValue = Stat.ATRIBUTE_BUFF_3;
		}
		if (user.getPosition () - 1 >= 0) {
			if (heroesTiles [user.getPosition () - 1].getOccupant () != null) {
				heroesTiles [user.getPosition () - 1].getOccupant ().DefBuff (defValue, duration);
			}
		}
		if (user.getPosition () + 1 <= 4) {
			if(heroesTiles[user.getPosition() + 1].getOccupant() != null){
				heroesTiles[user.getPosition() + 1].getOccupant().DefBuff(defValue,duration);
			}
		}
	}

	public override void OnMissedEffect (Character user, Battleground.Tile tile)
	{
		base.OnMissedEffect (user, tile);
	}
}
