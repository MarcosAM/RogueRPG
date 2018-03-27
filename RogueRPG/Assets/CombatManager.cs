using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour {

	public static Combatant[] initiativeList;
	public static int activeCombatant;

	public static event Action OnTurnEnded;
	public static event	Action OnHeroTurnBegin;

	void Start () {
		initiativeList = FindObjectsOfType<Combatant> ();
		activeCombatant = 0;
		initiativeList[activeCombatant].StartTurn ();
	}

	public static void NextTurn(){
		if(OnTurnEnded != null)
			OnTurnEnded();
		activeCombatant ++;
		initiativeList[activeCombatant%initiativeList.Length].StartTurn();
	}

	public static void StartHeroTurn ()
	{
		if(OnHeroTurnBegin != null)
			OnHeroTurnBegin();
	}

}
