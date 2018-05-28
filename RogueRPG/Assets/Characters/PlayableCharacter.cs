using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character {

	void Awake (){
		if(stats != null){
			FillStats ();
		}
		combatBehavior = GetComponent<ICombatBehavior> ();
		movement = GetComponent<IMovable> ();
		combatBehavior.Initialize (this);
		isHero = true;
		DontDestroyOnLoad (transform.parent.gameObject);
	}

}
