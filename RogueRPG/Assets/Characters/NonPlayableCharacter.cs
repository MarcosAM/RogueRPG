using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character {

	void Awake (){
		if(cStats != null){
			FillStats ();
		}
		cCombatBehavior = GetComponent<ICombatBehavior> ();
		cMovement = GetComponent<IMovable> ();
		cMovement.Initialize (this);
		cCombatBehavior.Initialize (this);
		cPlayable = false;
	}
}
