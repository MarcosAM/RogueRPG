using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Dungeon")]
public class Dungeon : ScriptableObject
{
    public enum State { Hidden, JustDiscovered, Open, Beaten }

    [SerializeField] string dungeonName;
    [SerializeField] List<BattleGroup> battleGroups = new List<BattleGroup>();
    [SerializeField] int levels;
    [SerializeField] int level;
    [SerializeField] int[] unlocks;
    [SerializeField] State state;

    public List<BattleGroup> getBattleGroups() { return battleGroups; }

    public BattleGroup GetRandomBattleGroup(int level)
    {
        //TODO depois comentar várias coisas aqui para eu ter uma noção das probabilidades de encontros aparecerem aqui
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
    public string GetDungeonName() { return dungeonName; }

    public State GetState() { return state; }
    public void SetState(State state) { this.state = state; }
    public int[] GetUnlocks() { return unlocks; }
}