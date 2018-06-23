using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DungeonManager : MonoBehaviour {

	List<Character> initiativeOrder = new List<Character>();
	int round;
	int dungeonFloor=0;
	Battleground battleground;

	void Start (){
		battleground = GetComponent<Battleground> ();
		GameManager gameManager = GameManager.getInstance ();
		battleground.ClearAndSetASide (gameManager.getPlayerCharacters ());
		battleground.ClearAndSetASide (gameManager.getEnemiesAtFloor(dungeonFloor));
		foreach (Character hero in battleground.getHeroSide()) {
			if(hero!=null)
				initiativeOrder.Add(hero);
		}
		foreach(Character enemy in battleground.getEnemySide()){
			if(enemy!=null)
				initiativeOrder.Add(enemy);
		}
		foreach(Character character in initiativeOrder){
			character.PrepareForFirstBattle ();
		}
		battleground.ShowCharactersToThePlayer ();
		round = 0;
		TryToStartTurn ();
	}

	void TryToStartTurn (){
		if (initiativeOrder.Count>0) {
			initiativeOrder [0].getBehavior().StartTurn();
		} else {
			StartCoroutine(WaitForNonDelayedCharacter());
		}
	}

	//TODO Definir uma variável de quanto em quanto eu recupero o delay
	IEnumerator WaitForNonDelayedCharacter(){
		bool WaitingForNonDelayedCharacter = true;
		while(WaitingForNonDelayedCharacter){

			for(int i=0;i<battleground.getHeroSide().Count;i++){
				if(battleground.getHeroSide()[i]!=null){
					battleground.getHeroSide () [i].RecoverFromDelayBy (0.1f);
					if (!battleground.getHeroSide () [i].IsDelayed ()) {
						ResumeBattleAfterDelayWith (battleground.getHeroSide()[i]);
						WaitingForNonDelayedCharacter = false;
						yield break;
					}
				}
			}

			for(int i=0;i<battleground.getEnemySide().Count;i++){
				if(battleground.getEnemySide()[i]!=null){
					battleground.getEnemySide () [i].RecoverFromDelayBy (0.1f);
					if (!battleground.getEnemySide () [i].IsDelayed ()) {
						ResumeBattleAfterDelayWith (battleground.getEnemySide ()[i]);
						WaitingForNonDelayedCharacter = false;
						yield break;
					}
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	void NextTurn(){
		if(DidOnePartyLost()){
			EndBattleAndCheckIfDungeonEnded ();
		} else if(initiativeOrder.Count>0){
			initiativeOrder.RemoveAt(0);
			round++;
			TryToStartTurn ();
		}
	}

	void ResumeBattleAfterDelayWith (Character combatant){
		initiativeOrder.Add(combatant);
		TryToStartTurn();
	}

	void DeleteFromInitiative (Character combatant){
		initiativeOrder.Remove(combatant);
	}

	void EndBattleAndCheckIfDungeonEnded (){
		dungeonFloor++;
		GameManager gameManager = GameManager.getInstance ();
		if (dungeonFloor < gameManager.getSelectedQuest ().getCurrentDungeon ().getBattleGroups ().Count) {
			battleground.ClearAndSetASide (gameManager.getEnemiesDelayedAtFloor(dungeonFloor));
			battleground.ShowCharactersToThePlayer ();
			initiativeOrder.RemoveAt(0);
			round ++;
			TryToStartTurn ();
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
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
		EventManager.OnDeathOf += DeleteFromInitiative;
	}
	void OnDisable(){
		EventManager.OnEndedTurn -= NextTurn;
		EventManager.OnDeathOf -= DeleteFromInitiative;
	}
}

//	IEnumerator RechargeEnergy (){
//		while (WaitingForNonDelayedCharacter){
//			EventManager.RechargeEnergy(0.1f);
//			yield return new WaitForSeconds(0.1f);
//		}
//	}
//		EventManager.OnRechargedEnergy += AddToInitiative;
//		EventManager.OnRechargedEnergy -= AddToInitiative;