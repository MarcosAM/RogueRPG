using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skill Effects/Assault")]
public class SEAssault : SkillEffect, IWaitForSkill {

	[SerializeField]protected SkillEffect firstEffect;
	[SerializeField]protected SkillEffect secondEffect;
	bool alreadyDidFirst = false;

	public override void startEffect(Character user, Battleground.Tile tile, IWaitForSkill requester){
		this.requester = requester;
		this.user = user;
		this.targetTile = tile;
		alreadyDidFirst = false;
//		user.getHUD ().playAnimation (this, "UseSkill");
		firstEffect.startEffect(user,tile,this);
	}

	public override bool WillBeAffected (Battleground.Tile target, Battleground.Tile tile)
	{
		if (firstEffect.WillBeAffected (DungeonManager.getInstance ().getBattleground ().getMyEnemiesTiles(target.isFromHero()) [target.getIndex ()], tile) || secondEffect.WillBeAffected (target, tile)) {
			return true;
		} else {
			return false;
		}
	}
	
	public void resumeFromSkill (){
		if (alreadyDidFirst) {
			endSkill ();
		} else {
			alreadyDidFirst = true;
			secondEffect.startEffect (user,targetTile,this);
		}
	}
}
