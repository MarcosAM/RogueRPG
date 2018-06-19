﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CombatManager : MonoBehaviour {

	private List<Character> initiativeOrder = new List<Character>();
	private int round;
	private bool WaitingForCombatantsToRechargeEnergy = false;
	int dungeonFloor;

	private Battleground battleground;

	void Start () {
		battleground = GetComponent<Battleground> ();
		GameManager gameManager = GameManager.getInstance ();
		foreach (Character character in gameManager.getPlayerCharacters()){
			initiativeOrder.Add (character);
		}
		gameManager.getSelectedQuest ().getCurrentDungeon ().getBattleGroups () [0].InitializeEnemies ();
		dungeonFloor = 0;
		foreach (Character character in gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups()[0].getEnemies()) {
			initiativeOrder.Add (character);
		}
//		FillInitiativeOrderWithAllCombatants();
		foreach(Character character in initiativeOrder){
			character.PrepareForNextCombat ();
		}
		battleground.PutCharactersInBattleground ();
		round = 0;
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
		dungeonFloor++;
		if (dungeonFloor > GameManager.getInstance ().getSelectedQuest ().getCurrentDungeon ().getBattleGroups ().Count) {
			print ("Passou!");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		} else {
			GameManager gameManager = GameManager.getInstance ();
			gameManager.getSelectedQuest ().getCurrentDungeon ().getBattleGroups () [dungeonFloor].InitializeEnemies ();
			foreach (Character character in gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups()[dungeonFloor].getEnemies()) {
				initiativeOrder.Add (character);
			}
			foreach(Character character in initiativeOrder){
				character.PrepareForNextCombat ();
			}
			battleground.PutCharactersInBattleground ();
			if(initiativeOrder.Count>0){
//				initiativeOrder.RemoveAt(0);
				round++;
				StartTurn ();
			}
		}
	}

	void FillInitiativeOrderWithAllCombatants (){
		Character[] temporaryList = FindObjectsOfType<Character>();
		for(int i = 0; i<temporaryList.Length; i++){
			initiativeOrder.Add(temporaryList[i]);
		}
	}

	bool DidOnePartyLost(){
		int countdown = 0;
		for(int i = 0;i<battleground.getHeroSide().Count;i++){
			if(battleground.getHeroSide()[i] != null){
				if(!battleground.getHeroSide()[i].isAlive()){
					countdown++;
				}
			}
		}
		if(countdown==battleground.HowManyHeroes()){
			return true;
		}
		countdown = 0;
		for(int i = 0;i<battleground.getEnemySide().Count;i++){
			if(battleground.getEnemySide()[i] != null){
				if(!battleground.getEnemySide()[i].isAlive()){
					countdown++;
				}
			}
		}
		if(countdown==battleground.HowManyEnemies()){
			return true;
		}
		return false;
	}

	void OnEnable(){
		EventManager.OnEndedTurn += NextTurn;
		EventManager.OnRechargedEnergy += AddToInitiative;
		EventManager.OnDeathOf += DeleteFromInitiative;
	}

	void OnDisable(){
		EventManager.OnEndedTurn -= NextTurn;
		EventManager.OnRechargedEnergy -= AddToInitiative;
		EventManager.OnDeathOf -= DeleteFromInitiative;
	}
}