using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombHUDManager : MonoBehaviour {

	[SerializeField]private CombatantHUD[] enemiesHUD = new CombatantHUD[4];
	[SerializeField]private CombatantHUD[] heroesHUD = new CombatantHUD[4];
	
	public void InitializeCombatantHUDs (Combatant[] h,Combatant[] e){
		for(int i = 0;i<h.Length;i++){
			heroesHUD[i].Initialize(h[i]);
		}
		for(int i = 0;i<e.Length;i++){
			enemiesHUD[i].Initialize(e[i]);
		}
	}

}
