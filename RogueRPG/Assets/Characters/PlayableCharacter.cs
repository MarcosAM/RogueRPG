using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character {

	void Awake (){
		atk = new Stat();
		atkm = new Stat();
		def = new Stat();
		defm = new Stat();
		critic = new Stat();
		precision = new Stat();
		dodge = new Stat();

		if(stats != null){
			FillStats ();
		}
		combatBehavior = GetComponent<ICombatBehavior> ();
		movement = GetComponent<IMovable> ();
		movement.Initialize (this);
		combatBehavior.Initialize (this);
		playable = true;
//		DontDestroyOnLoad (transform.parent.gameObject);
		DontDestroyOnLoad (gameObject);
	}

}
