using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombHUDManager : MonoBehaviour {

	[SerializeField]CombatantHUD combatantHUDprefab;

	[SerializeField] private Vector2[] heroesPositions = new Vector2[4];
	[SerializeField] private Vector2[] enemiesPositions = new Vector2[4];

	public void InitializeCombatantHUDs (Combatant[] h,Combatant[] e){
		for(int i = 0;i<h.Length;i++){
			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
			combatantHUD.transform.SetParent(transform,false);
			combatantHUD.getRectTransform().localPosition = heroesPositions[i];
			combatantHUD.Initialize(h[i]);
			h [i].setHUD (combatantHUD);
		}
		for(int i = 0;i<e.Length;i++){
			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
			combatantHUD.transform.SetParent(transform,false);
			combatantHUD.getRectTransform().localPosition = enemiesPositions[i];
			combatantHUD.Initialize(e[i]);
			e [i].setHUD (combatantHUD);
		}
	}

}
