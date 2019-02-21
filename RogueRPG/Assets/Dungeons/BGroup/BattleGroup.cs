using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/BattleGroup")]
public class BattleGroup : ScriptableObject
{

    [SerializeField] List<CharacterFactory> enemiesStats;
    public Battleground.BattlegroundSize battlegroundSize;

    public List<CharacterFactory> GetEnemiesStats() { return enemiesStats; }
    public Battleground.BattlegroundSize GetBattlegroundSize() { return battlegroundSize; }
}