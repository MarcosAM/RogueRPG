using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Quests/BattleGroup")]
public class BattleGroup : ScriptableObject {

	[SerializeField]List<StandartStats> enemiesStats;
	[SerializeField]List<float> enemiesDelay;
	[SerializeField]Character enemyPrefab;

	public List<Character> getEnemies(){
		List<Character> enemies = new List<Character> ();
		for(int i=0;i<enemiesStats.Count;i++){
			if (enemiesStats [i] != null) {
				enemies.Add (Instantiate (enemyPrefab));
				enemies [i].setStats (enemiesStats [i]);
			} else {
				enemies.Add (null);
			}
		}
		return enemies;
	}

	public List<Character> getEnemiesDelayed (){
		List<Character> enemies = getEnemies();
		for(int i=0;i<enemies.Count;i++){
			if(enemies[i]!=null){
				enemies[i].DelayBy(enemiesDelay[i]);
			}
		}
		return enemies;
	}
}