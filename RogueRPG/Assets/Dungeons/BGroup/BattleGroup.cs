using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/BattleGroup")]
public class BattleGroup : ScriptableObject
{

    [SerializeField] List<StandartStats> enemiesStats;
    public Battleground.BattlegroundSize battlegroundSize;

    public List<StandartStats> GetEnemiesStats() { return enemiesStats; }
    public Battleground.BattlegroundSize GetBattlegroundSize() { return battlegroundSize; }
}