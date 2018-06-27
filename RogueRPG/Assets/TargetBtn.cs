﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : CombatBtn {

	Text text;
	[SerializeField]Character combatant;
	Battleground.Tile tile;

	void Awake () {
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text>();
		Disappear ();
		button.onClick.AddListener (ChooseTarget);
	}

	void ChooseTarget(){
		EventManager.ChooseTarget (tile);
	}

	public void Initialize (Character c){
		combatant = c;
		text.text = c.getName();
		combatant.OnMyTurnStarts += ActiveCombatantOn;
		combatant.OnMyTurnEnds += ActiveCombatantOff;
	}

	public void Initialize (Battleground.Tile tile){
		this.tile = tile;
		if(this.tile.getOccupant()!=null){
			Initialize (this.tile.getOccupant());
		}
	}

	public void ActiveCombatantOn (){
		text.color = Color.blue;
	}

	public void ActiveCombatantOff(){
		text.color = new Color (0.2f,0.2f,0.2f,1f);
	}

	public void Appear (Character user, Skill skill)
	{
		//TODO Só aparecer quando tiver alvo para poder aparecer
		if (combatant.isAlive ()) {
			switch (skill.getTargets ()) {
			case Skill.Targets.Allies:
				if (combatant.isPlayable () && Mathf.Abs (combatant.getPosition () - user.getPosition ()) <= skill.getRange ()) {
					button.interactable = true;
					text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
				}
				break;
			case Skill.Targets.Enemies:
				if (!combatant.isPlayable () && Mathf.Abs (combatant.getPosition () - user.getPosition ()) <= skill.getRange ()) {
					button.interactable = true;
					text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
				}
				if (combatant.isPlayable ()) {
					if (combatant == user) {
						button.interactable = true;
						text.text = "Defender";
					} else {
						button.interactable = true;
						text.text = "Mover-se";
					}
				}
				break;
				case Skill.Targets.Self:
				if(combatant==user){
					button.interactable = true;
					text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				}
				break;
				case Skill.Targets.Location:
				break;
				default:
				button.interactable = true;
				text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				break;
			}
		}
	}

	public void Appear (Skill skill){
		//TODO Só aparecer quando tiver alvo para poder aparecer
		if (tile.getOccupant () != null) {
			if(tile.getOccupant().isAlive()){
				switch(skill.getTargets()){
				case Skill.Targets.Allies:
					if (tile.getOccupant ().isPlayable () && Mathf.Abs (tile.getOccupant ().getPosition () - skill.getUser ().getPosition ()) <= skill.getRange ()) {
						button.interactable = true;
						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
					}
					break;
				case Skill.Targets.Enemies:
					if (!tile.getOccupant ().isPlayable () && Mathf.Abs (tile.getOccupant ().getPosition () - skill.getUser ().getPosition ()) <= skill.getRange ()) {
						button.interactable = true;
						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
					}
					if (tile.getOccupant().isPlayable()) {
						if (tile.getOccupant() == skill.getUser()) {
							button.interactable = true;
							text.text = "Defender";
						} else {
							button.interactable = true;
							text.text = "Mover-se";
						}
					}
					break;
				case Skill.Targets.Self:
					if (tile.getOccupant () == skill.getUser ()) {
						button.interactable = true;
						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
					}
					break;
				default:
					button.interactable = true;
					text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
					break;
				}
			}
		}
		else {
			button.interactable = true;
			text.text = "Habilidade Secundária";
		}
	}

	override public void Disappear(){
		button.interactable = false;
		text.text = "";
	}

	void OnEnable(){
		EventManager.OnShowTargetsOf += Appear;
		EventManager.OnShowTargetsOf2 += Appear;
		EventManager.OnClickedTargetBtn += Disappear;
		EventManager.OnUnchoosedSkill += Disappear;
	}

	void OnDisable(){
		EventManager.OnShowTargetsOf -= Appear;
		EventManager.OnShowTargetsOf2 -= Appear;
		EventManager.OnClickedTargetBtn -= Disappear;
		EventManager.OnUnchoosedSkill -= Disappear;
		if(combatant != null){
			combatant.OnMyTurnStarts += ActiveCombatantOn;
			combatant.OnMyTurnEnds += ActiveCombatantOff;
		}
	}
}
