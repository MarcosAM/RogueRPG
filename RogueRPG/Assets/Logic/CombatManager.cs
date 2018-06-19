using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CombatManager : MonoBehaviour {

	private List<Character> initiativeOrder = new List<Character>();
	private int round;
	private bool WaitingForCombatantsToRechargeEnergy = false;
	int dungeonFloor=0;
	[SerializeField]Character enemyPrefab;

	private Battleground battleground;

	void Start (){
		battleground = GetComponent<Battleground> ();
		GameManager gameManager = GameManager.getInstance ();
		battleground.setHeroSide (gameManager.getPlayerCharacters ());
		battleground.getEnemySide ().Clear ();
		foreach (StandartStats enemiesStats in gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups()[dungeonFloor].getEnemiesStats()) {
			Character enemy = Instantiate (enemyPrefab);
			battleground.getEnemySide ().Add (enemy);
		}
		foreach (Character hero in battleground.getHeroSide()) {
			if(hero!=null)
				initiativeOrder.Add(hero);
		}
		foreach(Character enemy in battleground.getEnemySide()){
			if(enemy!=null)
				initiativeOrder.Add(enemy);
		}
//		foreach (Character character in gameManager.getPlayerCharacters()){
//			initiativeOrder.Add (character);
//		}
//		foreach(StandartStats enemiesStats in gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups()[dungeonFloor].getEnemiesStats()){
//			Character enemy = Instantiate(enemyPrefab);
//			initiativeOrder.Add(enemy);
//		}
		foreach(Character character in initiativeOrder){
			character.PrepareForFirstBattle ();
		}
		battleground.ShowCharactersToThePlayer ();
		round = 0;
		StartTurn ();
	}

	void StartTurn (){
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
			EndBattle ();
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

	void EndBattle (){
		dungeonFloor++;
		GameManager gameManager = GameManager.getInstance ();
		if (dungeonFloor > gameManager.getSelectedQuest ().getCurrentDungeon ().getBattleGroups ().Count) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		} else {
			battleground.getEnemySide ().Clear ();
			foreach (StandartStats enemiesStats in gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups()[dungeonFloor].getEnemiesStats()) {
				Character enemy = Instantiate (enemyPrefab);
				battleground.getEnemySide ().Add (enemy);
			}
			battleground.ShowCharactersToThePlayer ();
			initiativeOrder.RemoveAt(0);
			round ++;
			StartTurn ();
//			GameManager gameManager = GameManager.getInstance ();
//			gameManager.getSelectedQuest ().getCurrentDungeon ().getBattleGroups () [dungeonFloor].InitializeEnemies ();
//			foreach (Character character in gameManager.getSelectedQuest().getCurrentDungeon().getBattleGroups()[dungeonFloor].getEnemies()) {
//				initiativeOrder.Add (character);
//			}
//			foreach(Character character in initiativeOrder){
//				character.PrepareForFirstBattle ();
//			}
//			battleground.PutCharactersInBattleground ();
//			if(initiativeOrder.Count>0){
////				initiativeOrder.RemoveAt(0);
//				round++;
//				StartTurn ();
//			}
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