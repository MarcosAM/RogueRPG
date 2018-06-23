using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Magic Attacks")]
public class ATKMSkill : Skill {

	public override void UniqueEffect (Character user, Character target)
	{
		base.UniqueEffect (user, target);
		user.AttackMagic (target,sValue,this);
	}

//	public override void UniqueEffect (int targetIndex){
//		base.UniqueEffect (targetIndex);
//		DungeonManager dungeonManager = DungeonManager.getInstance();
//		Character target;
//		if(user.isPlayable()){
//			target
//		}
//		user.AttackMagic ();
//	}
}
