using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Dungeon")]
public class Dungeon : ScriptableObject
{
    [SerializeField] string dungeonName;
    [SerializeField] BattleGroup[] battleGroups;
    [SerializeField] int prerequisit;

    public BattleGroup[] GetBattleGroups() { return battleGroups; }

    public string GetDungeonName() { return dungeonName; }

    public int GetPrerequisit() { return prerequisit; }
}