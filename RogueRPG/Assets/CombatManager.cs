using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CombatManager : MonoBehaviour {

	private List<Character> initiativeOrder = new List<Character>();
	private int round;
	private bool WaitingForCombatantsToRechargeEnergy = false;

	private Character[] heroesParty;
	private Character[] enemiesParty;

	private ICombatDisplayer combatantHUDManager;

	void Start () {
		FillInitiativeOrderWithAllCombatants();
		foreach(Character character in initiativeOrder){
			character.PrepareForNextCombat ();
		}
		FillPartiesWithCombatants();
		round = 0;
		combatantHUDManager = FindObjectOfType<CombHUDManager>();
		combatantHUDManager.ShowCombatants(heroesParty,enemiesParty);
		StartTurn ();
	}

	void StartTurn ()
	{
		if (initiativeOrder.Count>0) {
			initiativeOrder [0].getBehavior().Act();
		} else {
			WaitingForCombatantsToRechargeEnergy = true;
			StartCoroutine(RechargeEnergy());
		}
	}

	IEnumerator RechargeEnergy (){
		while (WaitingForCombatantsToRechargeEnergy){
			EventManager.RechargeEnergy(0.1f);
			yield return new WaitForSeconds(0.1f);
		}
	}

	void NextTurn(){
		if(DidOnePartyLost()){
			EndCombat ();
		} else if(initiativeOrder.Count>0){
			initiativeOrder.RemoveAt(0);
			round++;
			StartTurn ();
		}
	}

	void AddToInitiative (Character combatant){
		initiativeOrder.Add(combatant);
		if(WaitingForCombatantsToRechargeEnergy == true){
			WaitingForCombatantsToRechargeEnergy = false;
			StartTurn();
		}
	}

	void DeleteFromInitiative (Character combatant){
		initiativeOrder.Remove(combatant);
	}

	void EndCombat (){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}

	void FillPartiesWithCombatants(){
		heroesParty = FindObjectsOfType<PlayableCharacter>();
		enemiesParty = FindObjectsOfType<NonPlayableCharacter>();
	}

	void FillInitiativeOrderWithAllCombatants (){
		Character[] temporaryList = FindObjectsOfType<Character>();
		for(int i = 0; i<temporaryList.Length; i++){
			initiativeOrder.Add(temporaryList[i]);
		}
	}

	bool DidOnePartyLost(){
		int countdown = 0;
		for(int i = 0;i<heroesParty.Length;i++){
			if(!heroesParty[i].isAlive()){
				countdown++;
			}
		}
		if(countdown==heroesParty.Length){
			return true;
		}
		countdown = 0;
		for(int i = 0;i<enemiesParty.Length;i++){
			if(!enemiesParty[i].isAlive()){
				countdown++;
			}
		}
		if(countdown==enemiesParty.Length){
			return true;
		}
		return false;
	}

	void OnEnable(){
		EventManager.OnEndedTurn += NextTurn;
		EventManager.OnRechargedEnergy += AddToInitiative;
		EventManager.OnDeathOf += DeleteFromInitiative;
//		EventManager.OnPartyLost += EndCombat;
	}

	void OnDisable(){
		EventManager.OnEndedTurn -= NextTurn;
		EventManager.OnRechargedEnergy -= AddToInitiative;
		EventManager.OnDeathOf -= DeleteFromInitiative;
//		EventManager.OnPartyLost -= EndCombat;
	}
}