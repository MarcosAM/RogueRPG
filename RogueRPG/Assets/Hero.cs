using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Combatant {

	public override void StartTurn(){
		base.StartTurn();
		EventManager.StartHeroTurn (this);
	}

	public override void EndTurn(){
	}

}
