using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Quests/BattleGroup")]
public class BattleGroup : ScriptableObject {

	[SerializeField]List<StandartStats> enemiesStats;

	public List<StandartStats> getEnemiesStats (){return enemiesStats;}
}