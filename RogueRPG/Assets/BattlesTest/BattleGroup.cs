using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Quests/BattleGroup")]
public class BattleGroup : ScriptableObject {

	[SerializeField]List<StandartStats> enemiesStats;
	[SerializeField]Character enemyPrefab;
//	List<Character> enemies;

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
//	public List<StandartStats> getEnemiesStats (){return enemiesStats;}
}