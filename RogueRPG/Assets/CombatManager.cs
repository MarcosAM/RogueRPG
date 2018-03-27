using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour {

	private Combatant[] initiativeList;
	[SerializeField]private Combatant activeCombatant;

	public static event Action OnTurnEnded;

	void Start () {
		initiativeList = FindObjectsOfType<Combatant> ();
		activeCombatant = initiativeList [0];
		activeCombatant.StartTurn ();
	}

//	public static void NextTurn(){
//		if()
//	}

}
