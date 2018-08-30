using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TargetBtn : CombatBtn, IPointerEnterHandler, IPointerExitHandler {

	Battleground.Tile tile;

	void Awake () {
		button = GetComponent<Button> ();
		Disappear ();
		button.onClick.AddListener (onClick);
	}

	public void setTile (Battleground.Tile tile){
		this.tile = tile;
	}

	void onClick (){
		CombHUDManager.getInstance().onTargetBtnPressed(tile);
	}

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
//		text.color = new Color (0.2f,0.2f,0.2f,1f);
		button.interactable = true;
		if (tile.getOccupant () != null) {
			if (user.isPlayable () == tile.isFromHero ()) {
				if (user == tile.getOccupant ()) {
					if (skill.getSelfEffect () != null) {
//						text.color = Color.blue;
//						button.interactable = true;
						ColorBlock cb = button.colors;
						cb.normalColor = new Color(button.colors.normalColor.r,button.colors.normalColor.g,button.colors.normalColor.b,1);
						cb.highlightedColor = new Color(button.colors.highlightedColor.r,button.colors.highlightedColor.g,button.colors.highlightedColor.b,1);
						button.colors = cb;
//						text.text = skill.getSelfEffect ().getEffectName ();
					}
				} else {
					if(skill.getAlliesEffect() != null){
						if(Mathf.Abs (tile.getIndex() - user.getPosition ()) <= skill.getAlliesEffect().getRange ()){
//							button.interactable = true;
							ColorBlock cb = button.colors;
							cb.normalColor = new Color(button.colors.normalColor.r,button.colors.normalColor.g,button.colors.normalColor.b,1);
							cb.highlightedColor = new Color(button.colors.highlightedColor.r,button.colors.highlightedColor.g,button.colors.highlightedColor.b,1);
							button.colors = cb;
//							text.text = skill.getAlliesEffect ().getEffectName ();
						}
					}
				}
			} else {
				if(tile.getOccupant ().isAlive ()){
					if (Mathf.Abs (tile.getOccupant ().getPosition () - user.getPosition ()) <= skill.getMeleeEffect ().getRange ()) {
						button.interactable = true;
						ColorBlock cb = button.colors;
						cb.normalColor = new Color(button.colors.normalColor.r,button.colors.normalColor.g,button.colors.normalColor.b,1);
						cb.highlightedColor = new Color(button.colors.highlightedColor.r,button.colors.highlightedColor.g,button.colors.highlightedColor.b,1);
						button.colors = cb;
//						text.text = skill.getMeleeEffect ().getEffectName ();
//						text.text += " " + user.getPrecisionOfSkillEffect (tile.getOccupant(),skill.getMeleeEffect())*100 + "%";
					} else {
						button.interactable = true;
						ColorBlock cb = button.colors;
						cb.normalColor = new Color(button.colors.normalColor.r,button.colors.normalColor.g,button.colors.normalColor.b,1);
						cb.highlightedColor = new Color(button.colors.highlightedColor.r,button.colors.highlightedColor.g,button.colors.highlightedColor.b,1);
						button.colors = cb;
//						text.text = skill.getRangedEffect ().getEffectName ();
//						text.text += " " + user.getPrecisionOfSkillEffect (tile.getOccupant(),skill.getRangedEffect())*100 + "%";
					}
				}
			}
		} else {
			if(user.isPlayable() == tile.isFromHero()){
				if(skill.getAlliesEffect() != null){
					if(Mathf.Abs (tile.getIndex() - user.getPosition ()) <= skill.getAlliesEffect().getRange ()){
//						button.interactable = true;
						ColorBlock cb = button.colors;
						cb.normalColor = new Color(button.colors.normalColor.r,button.colors.normalColor.g,button.colors.normalColor.b,1);
						cb.highlightedColor = new Color(button.colors.highlightedColor.r,button.colors.highlightedColor.g,button.colors.highlightedColor.b,1);
						button.colors = cb;
//						text.text = skill.getAlliesEffect ().getEffectName ();
					}
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
//		ColorBlock cb = button.colors;
//		cb.normalColor = new Color(button.colors.normalColor.r,button.colors.normalColor.g,button.colors.normalColor.b,0);
//		cb.highlightedColor = new Color(button.colors.highlightedColor.r,button.colors.highlightedColor.g,button.colors.highlightedColor.b,0);
//		button.colors = cb;
////		text.color = new Color (0.2f,0.2f,0.2f,1f);
//		if (tile != null) {
//			if (tile.getOccupant () != null) {
////				text.text = tile.getOccupant ().getName ();
////				if(DungeonManager.getInstance().getInitiativeOrder()[0] == tile.getOccupant())
////					text.color = Color.blue;
//			} else {
////				text.text = "";
//			}
//		} else {
////			text.text = "";
//		}
	}

	public void ShowItsActivePlayer (){
//		text.color = Color.blue;
	}

	public void TurnBackToBlack(){
//		text.color = new Color (0.2f,0.2f,0.2f,1f);
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		CombHUDManager.getInstance ().onTargetBtnHoverEnter (this);
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		CombHUDManager.getInstance ().onTargetBtnHoverExit (this);
	}

	public Battleground.Tile getTile() {return tile;}

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
