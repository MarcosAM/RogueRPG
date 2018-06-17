using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character {

	void Awake (){
		atk = new Stat();
		atkm = new Stat();
		def = new Stat();
		defm = new Stat();
		critic = new Stat();
		precision = new Stat();
		dodge = new Stat();
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
