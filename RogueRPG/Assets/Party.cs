using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {

	bool isEnemyParty;
	Combatant[] combatants;

	public void Initialize (bool e)
	{
		isEnemyParty = e;
		if (isEnemyParty) {
			combatants = FindObjectsOfType<Enemy> ();
		} else {
			combatants = FindObjectsOfType<Hero>();
		}
	}

	public Combatant[] getCombatants (){
		return combatants;
	}

}
