using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack")]
public class SEAtk : SkillEffect {

//	public override void UniqueEffect (Character user, Skill skill, Battleground.Tile tile)
//	{
//		base.UniqueEffect (user, skill, tile);
//		if (tile.getOccupant ())
//			user.Attack (tile.getOccupant(),value,skill);
//	}

//	public override void Effect (Character user, Battleground.Tile targetTile)
//	{
//		base.Effect (user, targetTile);
//		if(targetTile.getOccupant() != null){
//			if(targetTile.getOccupant().didIHitYouWith(getNewHitValue(user,targetTile))){
//				if (targetTile.getOccupant ().takeDamage (getNewDamage(user,value), source) > 0) {
//				}
//			}
//		}
//	}

	float attack;
	int dmg;

	public override void startEffect (Character user, Battleground.Tile tile, IWaitForSkill requester)
	{
		base.startEffect (user, tile, requester);
		attack = getAttack();
		dmg = (int)getDamage((int)value);
		this.requester = requester;
		this.user = user;
		this.targetTile = tile;
		playCastSkillAnimation();
	}

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		//			user.TryToHitWith (tile,this);
		if (tile.getOccupant ()) {
			if(didIHit(tile.getOccupant(), attack)){
				damage(tile.getOccupant(),dmg);
			}
		}
	}

	public override void onHitEffect (Character user, Battleground.Tile tile)
	{
		base.onHitEffect (user, tile);
//		user.HitWith (tile.getOccupant(),value,this);
//		damage(tile.getOccupant(),dmg);
	}

	public override void onMissedEffect (Character user, Battleground.Tile tile)
	{
		base.onMissedEffect (user, tile);
	}
}