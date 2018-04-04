using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {

	bool enemyParty;
	Combatant[] combatants;

	public void Initialize (bool e)
	{
		enemyParty = e;
		if (enemyParty) {
			combatants = FindObjectsOfType<Enemy> ();
		} else {
			combatants = FindObjectsOfType<Hero>();
		}
	}

	public Combatant[] getCombatants (){
		return combatants;
	}

	void CheckIfLost (Combatant c){
		bool found = false;
		for(int i = 0;i<combatants.Length;i++){
			if(combatants[i]==c){
				found = true;
				break;
			}
		}
		if(found){
			int deadMembers = 0;
			for(int i = 0;i<combatants.Length;i++){
				if(!combatants[i].isAlive()){
					deadMembers++;
				}
			}
			if(deadMembers == combatants.Length){
				EventManager.PartyLost(this);
			}
		}
	}

	public bool isEnemyParty (){
		return enemyParty;
	}

	void OnEnable (){
		EventManager.OnDeathOf += CheckIfLost;
	}

	void OnDisable (){
		EventManager.OnDeathOf -= CheckIfLost;
	}

}
