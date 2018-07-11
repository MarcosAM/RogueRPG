using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : CombatBtn {

	Text text;
//	[SerializeField]Character combatant;
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

//	public void Initialize (Character c){
//		combatant = c;
//		text.text = c.getName();
//		combatant.OnMyTurnStarts += ActiveCombatantOn;
//		combatant.OnMyTurnEnds += ActiveCombatantOff;
//	}

	public void Initialize (Battleground.Tile tile){
//		if(this.tile.getOccupant() != null){
//				tile.getOccupant().OnMyTurnStarts -= ActiveCombatantOn;
//				tile.getOccupant().OnMyTurnEnds -= ActiveCombatantOff;
//		}
		this.tile = tile;
		text.color = new Color (0.2f,0.2f,0.2f,1f);
		if(tile.getOccupant() != null){
			text.text = tile.getOccupant ().getName ();
//			tile.getOccupant().OnMyTurnStarts += ActiveCombatantOn;
//			tile.getOccupant().OnMyTurnEnds += ActiveCombatantOff;
		}
	}

//	public void ActiveCombatantOn (){
//		text.color = Color.blue;
//	}
//
//	public void ActiveCombatantOff(){
//		text.color = new Color (0.2f,0.2f,0.2f,1f);
//	}

//	public void Appear (Character user, Skill skill)
//	{
//		//TODO Só aparecer quando tiver alvo para poder aparecer
//		if(tile.getOccupant() != null){
//			if (tile.getOccupant().isAlive ()) {
//				switch (skill.getTargets ()) {
//				case Skill.Targets.Allies:
//					if (combatant.isPlayable () && Mathf.Abs (combatant.getPosition () - user.getPosition ()) <= skill.getRange ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					break;
//				case Skill.Targets.Enemies:
//					if (!combatant.isPlayable () && Mathf.Abs (combatant.getPosition () - user.getPosition ()) <= skill.getRange ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					if (combatant.isPlayable ()) {
//						if (combatant == user) {
//							button.interactable = true;
//							text.text = "Defender";
//						} else {
//							button.interactable = true;
//							text.text = "Mover-se";
//						}
//					}
//					break;
//				case Skill.Targets.Self:
//					if(combatant==user){
//						button.interactable = true;
//						text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
//					}
//					break;
//				case Skill.Targets.Location:
//					break;
//				default:
//					button.interactable = true;
//					text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
//					break;
//				}
//			}
//		}
//	}

	public void Appear (Character user, Skill skill)
	{
		//TODO Só aparecer quando tiver alvo para poder aparecer
//		if(tile.getOccupant() != null){
//			if (tile.getOccupant().isAlive ()) {
//				switch(skill.getTargets()){
//				case Skill.Targets.Allies:
//					if (tile.getOccupant ().isPlayable () && Mathf.Abs (tile.getOccupant ().getPosition () - user.getPosition()) <= skill.getRange ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					break;
//				case Skill.Targets.Enemies:
//					if (!tile.getOccupant ().isPlayable () && Mathf.Abs (tile.getOccupant ().getPosition () - user.getPosition ()) <= skill.getRange ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					if (tile.getOccupant().isPlayable()) {
//						if (tile.getOccupant() == user) {
//							button.interactable = true;
//							text.text = "Defender";
//						} else {
//							button.interactable = true;
//							text.text = "Mover-se";
//						}
//					}
//					break;
//				case Skill.Targets.Self:
//					if (tile.getOccupant () == user) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					break;
//				default:
//					button.interactable = true;
//					text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					break;
//				}
//			}
//		}
//		else {
//			button.interactable = true;
//			text.text = "Habilidade Secundária";
//		}
		text.color = new Color (0.2f,0.2f,0.2f,1f);
		if (tile.getOccupant () != null) {
			if (user.isPlayable () == tile.isFromHero ()) {
				if (user == tile.getOccupant ()) {
					if (skill.getSecondaryEffect () != null) {
						text.color = Color.blue;
						button.interactable = true;
						text.text = skill.getSecondaryEffect ().getEffectName ();
					}
				} else {
					if(skill.getTertiaryEffect() != null){
						button.interactable = true;
						text.text = skill.getTertiaryEffect ().getEffectName ();
					}
				}
			} else {
				if (Mathf.Abs (tile.getOccupant ().getPosition () - user.getPosition ()) <= skill.getPrimaryEffect().getRange () && tile.getOccupant().isAlive()) {
					if(skill.getPrimaryEffect() != null){
						button.interactable = true;
						text.text = skill.getPrimaryEffect ().getEffectName ();
					}
				}
			}
		} else {
			if(user.isPlayable() == tile.isFromHero()){
				if(skill.getTertiaryEffect() != null){
					button.interactable = true;
					text.text = skill.getTertiaryEffect ().getEffectName ();
				}
			}else{
				//NÃO APAREÇA
			}
		}
	}

//	public void Appear (Skill skill){
//		//TODO Só aparecer quando tiver alvo para poder aparecer
//		if (tile.getOccupant () != null) {
//			if(tile.getOccupant().isAlive()){
//				switch(skill.getTargets()){
//				case Skill.Targets.Allies:
//					if (tile.getOccupant ().isPlayable () && Mathf.Abs (tile.getOccupant ().getPosition () - skill.getUser ().getPosition ()) <= skill.getRange ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					break;
//				case Skill.Targets.Enemies:
//					if (!tile.getOccupant ().isPlayable () && Mathf.Abs (tile.getOccupant ().getPosition () - skill.getUser ().getPosition ()) <= skill.getRange ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					if (tile.getOccupant().isPlayable()) {
//						if (tile.getOccupant() == skill.getUser()) {
//							button.interactable = true;
//							text.text = "Defender";
//						} else {
//							button.interactable = true;
//							text.text = "Mover-se";
//						}
//					}
//					break;
//				case Skill.Targets.Self:
//					if (tile.getOccupant () == skill.getUser ()) {
//						button.interactable = true;
//						text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					}
//					break;
//				default:
//					button.interactable = true;
//					text.color = new Color (text.color.r, text.color.g, text.color.b, 1f);
//					break;
//				}
//			}
//		}
//		else {
//			button.interactable = true;
//			text.text = "Habilidade Secundária";
//		}
//	}

	override public void Disappear(){
		button.interactable = false;
		text.color = new Color (0.2f,0.2f,0.2f,1f);
		if (tile != null) {
			if (tile.getOccupant () != null) {
				text.text = tile.getOccupant ().getName ();
				if(DungeonManager.getInstance().getInitiativeOrder()[0] == tile.getOccupant())
					text.color = Color.blue;
			} else {
				text.text = "";
			}
		} else {
			text.text = "";
		}
	}

	public void ShowItsActivePlayer (){
		text.color = Color.blue;
	}

	public void TurnBackToBlack(){
		text.color = new Color (0.2f,0.2f,0.2f,1f);
	}

	void OnEnable(){
		EventManager.OnShowTargetsOf += Appear;
		EventManager.OnClickedTargetBtn += Disappear;
		EventManager.OnUnchoosedSkill += Disappear;
	}

	void OnDisable(){
		EventManager.OnShowTargetsOf -= Appear;
		EventManager.OnClickedTargetBtn -= Disappear;
		EventManager.OnUnchoosedSkill -= Disappear;
	}
}
