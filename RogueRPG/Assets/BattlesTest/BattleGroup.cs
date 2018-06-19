using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Quests/BattleGroup")]
public class BattleGroup : ScriptableObject {

	[SerializeField]List<StandartStats> enemiesStats;
	List<Character> enemies = new List<Character>();
	[SerializeField]Character enemyPrefab;

	public void InitializeEnemies(){
		enemies.Clear ();
		for(int i=0;i<enemiesStats.Count;i++){
			enemies.Add (Instantiate(enemyPrefab));
			enemies [i].setStats (enemiesStats[i]);
		}
	}

	public List<Character> getEnemies(){return enemies;}
}