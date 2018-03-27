using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant {

	public override void StartTurn ()
	{
		base.StartTurn ();
		EndTurn();
	}

	public override void EndTurn ()
	{
		base.EndTurn ();
		print(this.name +" Atacou!");
		CombatManager.NextTurn();
	}
}
