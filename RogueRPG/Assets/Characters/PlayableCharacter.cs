using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character {

	void Awake (){
		combatBehavior = GetComponent<ICombatBehavior> ();
		combatBehavior.Initialize (this);
		isHero = true;
	}

}
