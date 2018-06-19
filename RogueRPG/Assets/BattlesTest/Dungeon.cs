using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Quests/Dungeon")]
public class Dungeon : ScriptableObject {

	[SerializeField]List<BattleGroup> battleGroups = new List<BattleGroup> ();

	public List<BattleGroup> getBattleGroups(){return battleGroups;}
}