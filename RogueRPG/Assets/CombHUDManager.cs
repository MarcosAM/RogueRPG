using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombHUDManager : MonoBehaviour {

	[SerializeField]CombatantHUD combatantHUDprefab;

	CombatantHUD[] heroesCombatantHUD = new CombatantHUD[4];
	CombatantHUD[] enemiesCombatantHUD = new CombatantHUD[4];

	[SerializeField] private Vector2[] heroesPositions = new Vector2[4];
	[SerializeField] private Vector2[] enemiesPositions = new Vector2[4];

	//TESTE
	[SerializeField]SkillBtn[] skillsBtn = new SkillBtn[4];
	static CombHUDManager instance = null;

	public void Awake(){
		MakeItASingleton ();
		DontDestroyOnLoad (this.gameObject);
		CreateCombatantHUDs ();
	}

	public void CreateCombatantHUDs(){
		for(int i = 0; i<4;i++){
			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
			combatantHUD.transform.SetParent(transform,false);
			combatantHUD.getRectTransform().localPosition = heroesPositions[i];
			heroesCombatantHUD [i] = combatantHUD;
			heroesCombatantHUD [i].gameObject.SetActive(false);
		}
		for(int i = 0; i<4;i++){
			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
			combatantHUD.transform.SetParent(transform,false);
			combatantHUD.getRectTransform().localPosition = enemiesPositions[i];
			enemiesCombatantHUD [i] = combatantHUD;
			enemiesCombatantHUD [i].gameObject.SetActive (false);
		}
	}

//	public void ShowCharacterAt(Character character, int position){
//		if (character.isPlayable ()) {
//			heroesCombatantHUD [position].gameObject.SetActive (true);
//			heroesCombatantHUD [position].Initialize (character);
//			character.setHUD (heroesCombatantHUD [position]);
//		} else {
//			enemiesCombatantHUD [position].gameObject.SetActive (true);
//			enemiesCombatantHUD [position].Initialize (character);
//			character.setHUD (enemiesCombatantHUD [position]);
//		}
//	}

//	public void ShowCombatants (List<Character> heroesParty, List<Character> enemiesParty){
//		ClearCombatantsHUDs ();
//		for(int i = 0;i<heroesParty.Count;i++){
//			if(heroesParty[i]!=null){
//				heroesCombatantHUD [i].gameObject.SetActive (true);
//				heroesCombatantHUD [i].Initialize(heroesParty[i]);
//				heroesParty [i].setHUD (heroesCombatantHUD[i]);
//			}
//		}
//		for(int i = 0;i<enemiesParty.Count;i++){
//			if(enemiesParty[i]!=null){
//				enemiesCombatantHUD [i].gameObject.SetActive (true);
//				enemiesCombatantHUD [i].Initialize(enemiesParty[i]);
//				enemiesParty [i].setHUD (enemiesCombatantHUD[i]);
//			}
//		}
//	}

	public void ShowCombatants (Battleground.Tile[] heroesTiles, Battleground.Tile[] enemiesTiles){
		ClearCombatantsHUDs ();
		for(int i = 0;i<heroesTiles.Length;i++){
			heroesCombatantHUD [i].gameObject.SetActive (true);
			heroesCombatantHUD [i].Initialize(heroesTiles[i]);
			if(heroesTiles[i].getOccupant()!=null)
				heroesTiles[i].getOccupant().setHUD (heroesCombatantHUD[i]);
		}
		for(int i = 0;i<enemiesTiles.Length;i++){
			enemiesCombatantHUD [i].gameObject.SetActive (true);
			enemiesCombatantHUD [i].Initialize(enemiesTiles[i]);
			if(enemiesTiles[i].getOccupant()!=null)
				enemiesTiles[i].getOccupant().setHUD (enemiesCombatantHUD[i]);
		}
	}

	public void ClearCombatantsHUDs(){
		foreach (CombatantHUD combatantHUD in heroesCombatantHUD) {
			combatantHUD.Deinitialize();
			combatantHUD.gameObject.SetActive (false);
		}

		foreach (CombatantHUD combatantHUD in enemiesCombatantHUD) {
			combatantHUD.Deinitialize();
			combatantHUD.gameObject.SetActive (false);
		}
	}

	//TESTE
	public void ShowSkillsBtnOf(Character character){
		for(int i=0;i<skillsBtn.Length;i++){
			skillsBtn [i].RefreshSelf (character);
		}
	}

	void MakeItASingleton(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void RefreshInitiativeHUD (){
		foreach(CombatantHUD hud in heroesCombatantHUD){
			hud.RefreshInitiative();
		}
		foreach(CombatantHUD hud in enemiesCombatantHUD){
			hud.RefreshInitiative();
		}
	}

	public static CombHUDManager getInstance(){return instance;}
	public Vector2[] getHeroesPositions() {return heroesPositions;}
	public Vector2[] getEnemiesPositions() {return enemiesPositions;}
}