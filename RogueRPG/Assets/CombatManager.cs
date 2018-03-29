using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour {

	private Combatant[] initiativeList;
	private int round;
	private int activeCombatant;

	void Start () {
		initiativeList = FindObjectsOfType<Combatant> ();
		round = 0;
		activeCombatant = 0;
		StartTurn ();
	}

	void NextTurn(){
		round++;
		activeCombatant = round % initiativeList.Length;
		StartTurn ();
	}

	void StartTurn(){
		initiativeList[activeCombatant].StartTurn();
	}

	void OnEnable(){
		EventManager.OnEndedTurn += NextTurn;
	}

	void OnDisable(){
		EventManager.OnEndedTurn -= NextTurn;
	}

}
