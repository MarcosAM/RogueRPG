using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Combatant {

	public override void StartTurn(){
		EventManager.SetSkills ();
	}

	public override void EndTurn(){
	}

}
