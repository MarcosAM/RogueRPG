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
		round = 0;
		activeCombatant = 0;
		initiativeList[activeCombatant].StartTurn ();
	}

	void NextTurn(){
		initiativeList [activeCombatant].EndTurn ();
		round++;
		activeCombatant = round % initiativeList.Length;
		StartTurn ();
	}

	void StartTurn(){
		initiativeList[activeCombatant].StartTurn();
	}

	void StartHeroTurn ()
	{
		if(OnHeroTurnBegin != null)
			OnHeroTurnBegin();
	}

	void OnEnable(){
		EventManager.OnSkillUsed += NextTurn;
	}

	void OnDisable(){
		EventManager.OnSkillUsed -= NextTurn;
	}

}
