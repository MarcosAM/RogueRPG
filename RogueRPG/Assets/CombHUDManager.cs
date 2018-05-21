using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombHUDManager : MonoBehaviour, ICombatDisplayer {

	[SerializeField]CombatantHUD combatantHUDprefab;

	[SerializeField] private Vector2[] heroesPositions = new Vector2[4];
	[SerializeField] private Vector2[] enemiesPositions = new Vector2[4];

	public void ShowCombatants (Combatant[] heroesParty,Combatant[] enemiesParty){
		for(int i = 0;i<heroesParty.Length;i++){
			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
			combatantHUD.transform.SetParent(transform,false);
			combatantHUD.getRectTransform().localPosition = heroesPositions[i];
			combatantHUD.Initialize(heroesParty[i]);
			heroesParty [i].setHUD (combatantHUD);
		}
		for(int i = 0;i<enemiesParty.Length;i++){
			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
			combatantHUD.transform.SetParent(transform,false);
			combatantHUD.getRectTransform().localPosition = enemiesPositions[i];
			combatantHUD.Initialize(enemiesParty[i]);
			enemiesParty [i].setHUD (combatantHUD);
		}
	}

}
