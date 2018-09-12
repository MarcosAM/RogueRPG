using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TargetBtn : CombatBtn, IPointerEnterHandler, IPointerExitHandler {

	Battleground.Tile tile;
	public Image image;

	void Awake () {
		button = GetComponent<Button> ();
		Disappear ();
		button.onClick.AddListener (onClick);
//		image = GetComponent<Image> ();
	}

	public void setTile (Battleground.Tile tile){
		this.tile = tile;
	}

	void onClick (){
		CombHUDManager.getInstance().onTargetBtnPressed(tile);
	}

	public void Appear (Character user, Skill skill){

		SkillEffect skillEffect;
		if (tile.isFromHero () == user.isPlayable ()) {
			if (tile.getIndex () == user.getPosition ()) {
				skillEffect = skill.getSelfEffect ();
				image.color = new Color (0.309f, 0.380f, 0.674f, 1);
			} else {
				skillEffect = skill.getAlliesEffect ();
				image.color = new Color (0.952f, 0.921f, 0.235f, 1);
			}
		} else {
			if ((Mathf.Abs (tile.getIndex () - user.getPosition ()) <= skill.getMeleeEffect ().getRange ()) && tile.getOccupant () != null) {
				skillEffect = skill.getMeleeEffect ();
				image.color = new Color (0.925f, 0.258f, 0.258f, 1);
			} else {
				skillEffect = skill.getRangedEffect ();
				image.color = new Color (0.427f, 0.745f, 0.266f, 1);
			}
		}

		if (tile.getOccupant () != null) {
			if (tile.getOccupant ().isAlive () || (!tile.getOccupant ().isAlive () && skillEffect.canTargetDead ())) {
				image.gameObject.SetActive (true);
				button.interactable = true;
			} else {
				image.gameObject.SetActive (false);
				button.interactable = false;
			}
		} else {
			if (skill.getType () == Skill.Types.Ranged && !skillEffect.isSingleTarget ()) {
				image.gameObject.SetActive (true);
				button.interactable = true;
			} else {
				image.gameObject.SetActive (false);
				button.interactable = false;
			}
		}

//		image.gameObject.SetActive (true);
//		if (tile.getOccupant () != null) {
//			if (user.isPlayable () == tile.isFromHero ()) {
//				if (user == tile.getOccupant ()) {
//					if (skill.getSelfEffect () != null) {
//						button.interactable = true;
//						image.color = new Color (0.309f, 0.380f, 0.674f, 1);
//					}
//				} else {
//					if (skill.getAlliesEffect () != null) {
//						if (Mathf.Abs (tile.getIndex () - user.getPosition ()) <= skill.getAlliesEffect ().getRange ()) {
//							if ((skill.getAlliesEffect ().canTargetDead () && !tile.getOccupant ().isAlive ()) || tile.getOccupant ().isAlive ()) {
//								button.interactable = true;
//								image.color = new Color (0.952f, 0.921f, 0.235f, 1);
//							}
//						}
//					}
//				}
//			} else {
//				if (tile.getOccupant ().isAlive ()) {
//					if (Mathf.Abs (tile.getOccupant ().getPosition () - user.getPosition ()) <= skill.getMeleeEffect ().getRange ()) {
//						if ((skill.getMeleeEffect ().canTargetDead () && !tile.getOccupant ().isAlive ()) || tile.getOccupant ().isAlive ()) {
//							button.interactable = true;
//							image.color = new Color (0.925f, 0.258f, 0.258f, 1);
//						} else {
//							image.gameObject.SetActive (false);
//						}
//					} else {
//						if ((skill.getRangedEffect ().canTargetDead () && !tile.getOccupant ().isAlive ()) || tile.getOccupant ().isAlive ()) {
//							button.interactable = true;
//							image.color = new Color (0.427f, 0.745f, 0.266f, 1);
//						} else {
//							image.gameObject.SetActive (false);
//						}
//					}
//				}
//			}
//		} else {
//			if (user.isPlayable () == tile.isFromHero ()) {
//				if (skill.getAlliesEffect () != null) {
//					if (Mathf.Abs (tile.getIndex () - user.getPosition ()) <= skill.getAlliesEffect ().getRange () && skill.getAlliesEffect ().canTargetTile ()) {
//						button.interactable = true;
//						image.color = new Color (0.952f, 0.921f, 0.235f, 1);
//					} else {
//						image.gameObject.SetActive (false);
//					}
//				}
//			} else {
//				if (skill.getMeleeEffect ().canTargetTile () && Mathf.Abs (tile.getOccupant ().getPosition () - user.getPosition ()) <= skill.getMeleeEffect ().getRange ()) {
//					button.interactable = true;
//					image.color = new Color (0.925f, 0.258f, 0.258f, 1);
//				} else {
//					if(skill.getRangedEffect().canTargetTile()){
//						button.interactable = true;
//						image.color = new Color (0.427f, 0.745f, 0.266f, 1);
//					}
//				}
//			}
//		}
	}

	public void show (Color color){
		image.gameObject.SetActive(true);
		button.interactable = false;
		image.color = color;
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
		image.gameObject.SetActive(false);
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
