using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character {

	void Awake (){
		if(stats != null){
			FillStats ();
		}
		combatBehavior = GetComponent<ICombatBehavior> ();
		combatBehavior.Initialize (this);
		isHero = false;
	}
}
