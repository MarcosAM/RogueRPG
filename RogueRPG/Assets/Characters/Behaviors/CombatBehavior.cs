using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour {

	[SerializeField]protected Character character;
	protected Skill choosedSkill;
	protected Battleground.Tile targetTile;
	protected List<TurnAction> possibleActions = new List<TurnAction>();

	public virtual void StartTurn(){}
	public virtual void UseSkill(){}
	public virtual void skillBtnPressed (Skill skill){}
	public virtual void targetBtnPressed (Battleground.Tile targetTile){}
	public virtual void unchooseSkill (){}

	public void setCharacter(Character character) {
		this.character = character;
		if(possibleActions != null){
			for(int i=0; i < possibleActions.Count; i++){
				possibleActions [i].setCharacter (character);
			}
		}
	}
	public Character getCharacter() {return character;}
	public Skill getChoosedSkill() {return choosedSkill;}
	public void setChoosedSkill(Skill skill) {this.choosedSkill = skill;}
	public void setTargetTile(Battleground.Tile tile) {this.targetTile = tile;}
	public Battleground.Tile getTargetTile() {return targetTile;}
}