using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Attack")]
public class SEAtk : Skill {

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

	public override void StartSkill (Character user, Battleground.Tile tile, IWaitForSkill requester)
	{
		base.StartSkill (user, tile, requester);
		attack = GetAttack();
		dmg = (int)GetDamage((int)value);
		this.requester = requester;
		this.currentUser = user;
		this.targetTile = tile;
		PlayCastSkillAnimation();
	}

	public override void UniqueEffect (Character user, Battleground.Tile tile)
	{
		base.UniqueEffect (user, tile);
		//			user.TryToHitWith (tile,this);
		if (tile.getOccupant ()) {
			if (WasCritic()) {
				Damage(tile.getOccupant(),dmg,true);
			} else {
				if(DidIHit(tile.getOccupant(), attack)){
					Damage(tile.getOccupant(),dmg,false);
				}
			}
		}
	}

	public override void OnHitEffect (Character user, Battleground.Tile tile)
	{
		base.OnHitEffect (user, tile);
//		user.HitWith (tile.getOccupant(),value,this);
//		damage(tile.getOccupant(),dmg);
	}

	public override void OnMissedEffect (Character user, Battleground.Tile tile)
	{
		base.OnMissedEffect (user, tile);
	}
}