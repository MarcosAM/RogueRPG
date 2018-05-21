using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour {

//	[SerializeField]private Combatant[] initiativeList;
	[SerializeField]private List<Combatant> initiativeOrder;
	private int round;
	private int activeCombatant;
	private bool WaitingForCombatantsToRechargeEnergy = false;

	private Combatant[] heroeParty;
	private Combatant[] enemieParty;

	[SerializeField]private Party heroesParty;
	[SerializeField]private Party enemiesParty;

	private CombHUDManager combatantHUDManager;

	void Start () {
		FillInitiativeOrderWithAllCombatants();
//		initiativeList = FindObjectsOfType<Combatant> ();
		round = 0;
		activeCombatant = 0;

		FillPartiesWithCombatants();
//		TODO Será que e deveria substituir objetos da classe Party por apenas arrays de combatants
		heroesParty.Initialize(false);
		enemiesParty.Initialize(true);

		combatantHUDManager = FindObjectOfType<CombHUDManager>();
		combatantHUDManager.InitializeCombatantHUDs(heroesParty.getCombatants(),enemiesParty.getCombatants());
		StartTurn ();
	}

	void NextTurn(){
		if(initiativeOrder.Count>0){
			initiativeOrder.RemoveAt(0);
			round++;
			StartTurn ();
		}
	}

	IEnumerator RechargeEnergy (){
		while (WaitingForCombatantsToRechargeEnergy){
			EventManager.RechargeEnergy(0.1f);
			yield return new WaitForSeconds(0.1f);
		}
	}

	void AddToInitiative (Combatant combatant){
		initiativeOrder.Add(combatant);
		if(WaitingForCombatantsToRechargeEnergy = true){
			WaitingForCombatantsToRechargeEnergy = false;
			StartTurn();
		}
	}

	void DeleteFromInitiative (Combatant combatant){
		initiativeOrder.Remove(combatant);
	}

	void StartTurn ()
	{
		if (initiativeOrder.Count>0) {
			initiativeOrder [0].StartTurn ();
		} else {
			WaitingForCombatantsToRechargeEnergy = true;
			StartCoroutine(RechargeEnergy());
		}
	}

	void EndCombat (Party loosers)
	{
		if (loosers.isEnemyParty ()) {
			print ("Heróis ganharam!");
		} else {
			print ("Vilões ganharam!");
		}
	}

	void FillPartiesWithCombatants(){
		heroeParty = FindObjectsOfType<Hero>();
		enemieParty = FindObjectsOfType<Enemy>();
	}

	void FillInitiativeOrderWithAllCombatants (){
		Combatant[] temporaryList = FindObjectsOfType<Combatant>();
		for(int i = 0; i<temporaryList.Length; i++){
			initiativeOrder.Add(temporaryList[i]);
		}
	}

	void OnEnable(){
		EventManager.OnEndedTurn += NextTurn;
		EventManager.OnRechargedEnergy += AddToInitiative;
		EventManager.OnDeathOf += DeleteFromInitiative;
		EventManager.OnPartyLost += EndCombat;
	}

	void OnDisable(){
		EventManager.OnEndedTurn -= NextTurn;
		EventManager.OnRechargedEnergy -= AddToInitiative;
		EventManager.OnDeathOf -= DeleteFromInitiative;
		EventManager.OnPartyLost -= EndCombat;
	}
}