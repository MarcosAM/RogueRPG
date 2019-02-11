using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Dungeon")]
public class Dungeon : ScriptableObject
{
    public enum State { Hidden, JustDiscovered, Open, Beaten }

    [SerializeField] string dungeonName;
    //[SerializeField] BattleGroup[] battleGroups;
    //[SerializeField] BattleGroup[][] test;
    [SerializeField] BattleGroups[] battleGroups;
    [SerializeField] int level;
    [SerializeField] int[] unlocks;
    [SerializeField] State state;

    public BattleGroup[] GetRandomBattleGroups() { return battleGroups[Random.Range(0, battleGroups.Length - 1)].groups; }

    public int GetLevel() { return level; }
    public string GetDungeonName() { return dungeonName; }

    public State GetState() { return state; }
    public void SetState(State state) { this.state = state; }
    public int[] GetUnlocks() { return unlocks; }
}