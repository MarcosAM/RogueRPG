using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Quests/Quest")]
public class Quest : ScriptableObject {

	[SerializeField]Dungeon currentDungeon;

	public Dungeon getCurrentDungeon(){return currentDungeon;}
}