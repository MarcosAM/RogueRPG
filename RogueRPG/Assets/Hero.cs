using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Combatant {

	public override void StartTurn(){
		base.StartTurn();
		CombatManager.StartHeroTurn();
		EventManager.SetSkills ();
	}

	public override void EndTurn(){
	}

}
