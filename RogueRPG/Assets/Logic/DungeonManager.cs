using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class DungeonManager : MonoBehaviour {

	static DungeonManager instance = null;
	[SerializeField]List<Character> initiativeOrder = new List<Character>();
	int round;
	int dungeonFloor=0;
	Battleground battleground;
	[SerializeField]Character characterPrefab;
	[SerializeField]MomentumBar momentumBar;
	float momentum = 0;
	[SerializeField]float maxMomentum;
	int currentMomentumDowntime = 0;
	int maxMomentumDowntime = 4;

	void Start (){
		MakeItASingleton();
		momentumBar.Initialize (maxMomentum);
		battleground = GetComponent<Battleground> ();
		GameManager gameManager = GameManager.getInstance ();
		List<Character> pcs = new List<Character>();
		for(int i = 0;i<gameManager.getHeroesStats().Count;i++){
			Character pc = Instantiate(characterPrefab);
			pc.setStats(gameManager.getHeroesStats()[i]);
			pc.setName(gameManager.getHeroesNames()[i]);
			pcs.Add(pc);
		}
		battleground.ClearAndSetASide (pcs);
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
			character.refresh ();
		}
//		for(int i=0;i<initiativeOrder.Count;i++){
//			initiativeOrder [i].RecoverFromDelayBy ((float)(initiativeOrder.Count-i)/100);
//		}
		battleground.ShowCharactersToThePlayer ();
		CombHUDManager.getInstance().RefreshInitiativeHUD();
		round = 0;
		TryToStartTurn ();
	}

	void TryToStartTurn (){
//		string initiative="";
//		for(int i=0;i<initiativeOrder.Count;i++){
//			if (i == 0)
//				initiative += "Rodada " + round+": ";
//			initiative += initiativeOrder[i].getName()+": "+initiativeOrder [i].getDelayCountdown () + ",";
//		}
//		print (initiative);
		if (initiativeOrder.Count>0) {
			manageMomentum ();
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
		if(DidOnePartyLost() > 0){
			EndBattleAndCheckIfDungeonEnded ();
			return;
		}
		if(DidOnePartyLost() == 0){
			if(initiativeOrder.Count>0){
				AdvanceInitiative(initiativeOrder);
				CombHUDManager.getInstance().RefreshInitiativeHUD();
				round++;
				TryToStartTurn ();
				return;
			}
		}
		if(DidOnePartyLost() < 0){
			SceneManager.LoadScene (3);
			return;
		}
	}

	public void AdvanceInitiative (List<Character> initiative)
	{
//		for(int i=1;i<initiative.Count;i++){
//			initiative [i].RecoverFromDelayBy (1);
//			}
//
//		for(int i=1; i<initiative.Count; i++){
//			if(initiative[0].CompareTo(initiative[i]) > 0){
//				initiative.Insert (i-1,initiative[0]);
//				initiative.RemoveAt (0);
//					break;
//				}
//			if (initiative [0].CompareTo (initiative [i]) <= 0 && i == (initiative.Count - 1)) {
//				initiative.Add (initiative[0]);
//				initiative.RemoveAt (0);
//					break;
//				}
//			}
	initiative.Add(initiative[0]);
	initiative.RemoveAt(0);
	}

	void ResumeBattleAfterDelayWith (Character combatant){
		initiativeOrder.Add(combatant);
		TryToStartTurn();
	}

	void DeleteFromInitiative (Character combatant){
		initiativeOrder.Remove(combatant);
	}

	public void addToInitiative (Character character){
		initiativeOrder.Add(character);
	}

	void EndBattleAndCheckIfDungeonEnded (){
		dungeonFloor++;
		GameManager gameManager = GameManager.getInstance ();
		if (dungeonFloor < gameManager.getSelectedQuest ().getCurrentDungeon ().getBattleGroups ().Count) {
			AdvanceInitiative(initiativeOrder);
//			battleground.ClearAndSetASide (gameManager.getEnemiesDelayedAtFloor(dungeonFloor));
			battleground.ClearAndSetASide (gameManager.getEnemiesAtFloor(dungeonFloor));
			battleground.ShowCharactersToThePlayer ();
			foreach(Character character in battleground.getEnemySide()){
				if(character != null)
					initiativeOrder.Add (character);
			}
//			initiativeOrder.RemoveAt(0);
//			AdvanceInitiative(initiativeOrder);
			CombHUDManager.getInstance().RefreshInitiativeHUD();
			round ++;
			TryToStartTurn ();
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}

	int DidOnePartyLost(){
		int countdown = 0;
		for(int i = 0;i<battleground.getHeroSide().Count;i++){
			if(battleground.getHeroSide()[i] != null){
				if(!battleground.getHeroSide()[i].isAlive()){
					countdown++;
				}
			}
		}
		if(countdown==battleground.HowManyHeroes()){
			return -1;
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
			return 1;
		}
		return 0;
	}

	void MakeItASingleton(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public static DungeonManager getInstance () {return instance;}
	public Battleground getBattleground () {return battleground;}
	public List<Character> getInitiativeOrder (){return initiativeOrder;}
	public int getRound(){return round;}

	public bool shouldMomentumStop(){
		currentMomentumDowntime++;
		if (currentMomentumDowntime >= maxMomentumDowntime) {
			currentMomentumDowntime = 0;
			return true;
		} else {
			return false;
		}
	}

	public void manageMomentum(){
		if (momentum >= maxMomentum) {
			if (shouldMomentumStop ()) {
				addMomentum (-70);
			}
		} else {
			addMomentum (-10/initiativeOrder.Count);
		}
	}
	public void addMomentum(float amount){
		momentum += amount;
		momentumBar.changeMomentumBy (amount);
		if (momentum > maxMomentum)
			momentum = maxMomentum;
		if (momentum < 0)
			momentum = 0;
	}
	public bool isMomentumFull(){
		if (momentum >= maxMomentum) {
			return true;
		} else {
			return false;
		}
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