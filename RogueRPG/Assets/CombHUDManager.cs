using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombHUDManager : MonoBehaviour, ICombatDisplayer {

	[SerializeField]CombatantHUD combatantHUDprefab;

	CombatantHUD[] heroesCombatantHUD = new CombatantHUD[4];
	CombatantHUD[] enemiesCombatantHUD = new CombatantHUD[4];

	[SerializeField] private Vector2[] heroesPositions = new Vector2[4];
	[SerializeField] private Vector2[] enemiesPositions = new Vector2[4];

	public void Awake(){
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

	public void ShowCombatants (Character[] heroesParty,Character[] enemiesParty){

		foreach (CombatantHUD combatantHUD in heroesCombatantHUD) {
			combatantHUD.gameObject.SetActive (false);
		}

		foreach (CombatantHUD combatantHUD in enemiesCombatantHUD) {
			combatantHUD.gameObject.SetActive (false);
		}

		for(int i = 0;i<heroesParty.Length;i++){
			heroesCombatantHUD [i].gameObject.SetActive (true);
			heroesCombatantHUD [i].Initialize(heroesParty[i]);
			heroesParty [i].setHUD (heroesCombatantHUD[i]);
		}
		for(int i = 0;i<enemiesParty.Length;i++){
			enemiesCombatantHUD [i].gameObject.SetActive (true);
			enemiesCombatantHUD [i].Initialize(enemiesParty[i]);
			enemiesParty [i].setHUD (enemiesCombatantHUD[i]);
		}
	}

}
