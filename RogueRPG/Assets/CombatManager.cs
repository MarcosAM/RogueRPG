using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour {

	private Combatant[] initiativeList;
	private int round;
	private int activeCombatant;

	public static event Action OnTurnEnded;
	public static event	Action OnHeroTurnBegin;

	void Start () {
		initiativeList = FindObjectsOfType<Combatant> ();
		activeCombatant = 0;
		initiativeList[activeCombatant].StartTurn ();
	}

	void NextTurn(){
		activeCombatant ++;
		StartTurn ();
	}

	void StartTurn(){
		initiativeList[activeCombatant%initiativeList.Length].StartTurn();
	}

	void StartHeroTurn ()
	{
		if(OnHeroTurnBegin != null)
			OnHeroTurnBegin();
	}

}
