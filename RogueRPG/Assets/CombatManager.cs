using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour {

	[SerializeField]private Combatant[] initiativeList;
	private int round;
	private int activeCombatant;
	private bool isReachargingEnergy=false;

	[SerializeField]private Party heroesParty;
	[SerializeField]private Party enemiesParty;

	private CombHUDManager combatantHUDManager;

	void Start () {
		//TODO Ajustar initiativeList e Party para caso os inimigos comecem a dar spawn em outros inimigos.
		initiativeList = FindObjectsOfType<Combatant> ();
		round = 0;
		activeCombatant = 0;
		heroesParty.Initialize(false);
		enemiesParty.Initialize(true);
		combatantHUDManager = FindObjectOfType<CombHUDManager>();
		combatantHUDManager.InitializeCombatantHUDs(heroesParty.getCombatants(),enemiesParty.getCombatants());
		StartTurn ();
	}

	void NextTurn(){
		initiativeList[activeCombatant] = null;
		round++;
		activeCombatant = round % initiativeList.Length;
		StartTurn ();
	}

	IEnumerator RechargeEnergy (){
		while (isReachargingEnergy){
			EventManager.RechargeEnergy(0.1f);
			yield return new WaitForSeconds(0.1f);
		}
	}

	void AddToInitiative (Combatant c){
		int i = round;
		while (initiativeList[i%initiativeList.Length]!=null){
			i++;
		}
		initiativeList[i%initiativeList.Length] = c;
		isReachargingEnergy = false;
		StartTurn();
	}

	void StartTurn ()
	{
		if (initiativeList [activeCombatant] != null) {
			print("Array número: "+activeCombatant);
			initiativeList [activeCombatant].StartTurn ();
		}
		else {
			isReachargingEnergy = true;
			StartCoroutine(RechargeEnergy());
		}
	}

	void OnEnable(){
		EventManager.OnEndedTurn += NextTurn;
		EventManager.OnRechargedEnergy += AddToInitiative;
	}

	void OnDisable(){
		EventManager.OnEndedTurn -= NextTurn;
		EventManager.OnRechargedEnergy -= AddToInitiative;
	}

}
