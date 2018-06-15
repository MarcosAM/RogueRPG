using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character {

	void Awake (){
		if(cStats != null){
			FillStats ();
		}
		cCombatBehavior = GetComponent<ICombatBehavior> ();
		cMovement = GetComponent<IMovable> ();
		cMovement.Initialize (this);
		cCombatBehavior.Initialize (this);
		cPlayable = true;
		DontDestroyOnLoad (transform.parent.gameObject);
	}

}
