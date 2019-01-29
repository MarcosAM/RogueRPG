using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Dungeon")]
public class Dungeon : ScriptableObject
{

    [SerializeField] List<BattleGroup> battleGroups = new List<BattleGroup>();
    [SerializeField] int levels;
    [SerializeField] int level;

    public List<BattleGroup> getBattleGroups() { return battleGroups; }

    public BattleGroup GetRandomBattleGroup(int level)
    {
        if (level <= levels / 3)
        {
            return battleGroups[Random.Range(0, (battleGroups.Count - 1) / 3)];
        }

        if (level > levels * 2 / 3)
        {
            return battleGroups[Random.Range((battleGroups.Count - 1) * 2 / 3, battleGroups.Count - 1)];
        }

        return battleGroups[Random.Range((battleGroups.Count - 1) / 3, (battleGroups.Count - 1) * 2 / 3)];
    }

    public int GetLevels() { return levels; }
    public int GetLevel() { return level; }
}