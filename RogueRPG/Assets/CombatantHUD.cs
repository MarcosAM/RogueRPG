using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantHUD : MonoBehaviour, IPlayAnimationByString {

	[SerializeField]private Character combatant;
	[SerializeField]private Slider hpBar;
	[SerializeField]private Slider staminaBar;
	[SerializeField]private Text hpNumbers;
	[SerializeField]private TargetBtn targetButton;
	[SerializeField]private Image image;
//	[SerializeField]private Image emptyImage;
	[SerializeField]private Text buffText;
	[SerializeField]private DamageFB damageFbPrefab;
	[SerializeField]private Text initiative;
	[SerializeField]private Text name;
	private RectTransform rectTransform;
	[SerializeField]RectTransform portraitHandler;
	[SerializeField]RectTransform frontHandler;
	[SerializeField]RectTransform backHandler;
	[SerializeField]private Image equipment;
	Animator animator;
	IWaitForAnimationByString requester;

	void Awake(){
//		image = GetComponentInChildren<Image> ();
//		image.sprite = emptyImage.sprite;
		rectTransform = GetComponent<RectTransform> ();
		animator = GetComponent<Animator> ();
	}

//	public void Initialize (Character c)
//	{
//		if (c != null) {
//			combatant = c;
//			image.sprite = combatant.getPortrait ().sprite;
//			hpBar.gameObject.SetActive(true);
//			energyBar.gameObject.SetActive(true);
//			hpNumbers.gameObject.SetActive(true);
//			buffText.gameObject.SetActive (true);
//			combatant.OnHUDValuesChange += Refresh;
//			combatant.OnHPValuesChange += HPFeedback;
//			combatant.OnBuffsGainOrLoss += ShowBuffs;
//			//TODO Checar se isso aqui não vai cagar tudo com os buffs dos personagens quando eles se moverem
//			ShowBuffs ();
//			Refresh ();
//			if (targetButton != null)
//				targetButton.Initialize (combatant);
//		} 
//	}

	public void Initialize(Battleground.Tile tile){
		if (tile.isFromHero ()) {
//			portraitHandler.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
//			rectTransform.anchorMax = new Vector2(0.5f,0f);
//			rectTransform.anchorMin = new Vector2(0.5f,0f);
			hpNumbers.gameObject.SetActive(true);
		} else {
//			portraitHandler.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
			portraitHandler.localScale = new Vector3 (-1,1,1);
//			image.rectTransform.localScale = new Vector3 (-1,1,1);
			hpNumbers.gameObject.SetActive(false);
//			rectTransform.anchorMax = new Vector2(0.5f,1f);
//			rectTransform.anchorMin = new Vector2(0.5f,1f);
		}
		if(tile.getOccupant() != null){
			combatant = tile.getOccupant();
			image.gameObject.SetActive(true);
			image.sprite = combatant.getPortrait ().sprite;
			hpBar.gameObject.SetActive(true);
//			staminaBar.gameObject.SetActive(true);
//			buffText.gameObject.SetActive (true);
			name.gameObject.SetActive(true);
			name.text = combatant.getName();
			combatant.OnHUDValuesChange += Refresh;
			combatant.OnHPValuesChange += HPFeedback;
			combatant.OnBuffsGainOrLoss += ShowBuffs;
			//TODO Checar se isso aqui não vai cagar tudo com os buffs dos personagens quando eles se moverem
			ShowBuffs ();
			Refresh ();
		} else {
			name.gameObject.SetActive(false);
			this.combatant = null;
			image.sprite = null;
			image.gameObject.SetActive(false);
//			image.sprite = emptyImage.sprite;
			hpBar.gameObject.SetActive(false);
//			staminaBar.gameObject.SetActive(false);
			hpNumbers.gameObject.SetActive(false);
//			buffText.gameObject.SetActive (false);
			if (targetButton != null)
				targetButton.Disappear ();
		}
		targetButton.setTile (tile);
	}

	public void Deinitialize(){
		if(combatant != null){
			combatant.OnHUDValuesChange -= Refresh;
			combatant.OnHPValuesChange -= HPFeedback;
			combatant.OnBuffsGainOrLoss -= ShowBuffs;
			combatant = null;
		}
//		targetButton.Initialize (null);
	}

	public void HPFeedback(int pastHp, int amountChanged){
		DamageFB damageFb = Instantiate (damageFbPrefab);
		damageFb.transform.SetParent (transform.parent,false);
		damageFb.getRectTransform ().localPosition = rectTransform.localPosition + Vector3.right*50;
		damageFb.Initialize (amountChanged);
	}

	public void setHpBar (float v){
		if(v >= 0 && v <= 1)
			hpBar.value = v;
	}

//	public void setStaminaBar(float percentege){
//		if (percentege >= 0 && percentege <= 1)
//			staminaBar.value = percentege;
//	}

	public void setHpNumbers (float hp, float maxHp){
		hpNumbers.text = hp +"/"+maxHp;
	}

	public void Refresh (){
		setHpBar (combatant.getHp () / combatant.getMaxHp ());
		setHpNumbers (combatant.getHp (), combatant.getMaxHp ());
//		setStaminaBar (combatant.getCurrentStamina() / combatant.getMaxStamina());
//		UpdateEnergyBar();
	}

	void ShowBuffs (){
		//TODO Fazer isso de forma automatica criando uma variável para o nome do stat
//		buffText.text = "";
//		if(combatant.getAtk().getBuffValue() != 0){
//			buffText.text += "ATK " + combatant.getAtk().getBuffValue() + " ";
//		}
//		if(combatant.getAtkm().getBuffValue() != 0){
//			buffText.text += "ATKM " + combatant.getAtkm().getBuffValue() + " ";
//		}
//		if(combatant.getDef().getBuffValue() != 0){
//			buffText.text += "DEF " + combatant.getDef().getBuffValue() + " ";
//		}
//		if(combatant.getDefm().getBuffValue() != 0){
//			buffText.text += "DEFM " + combatant.getDefm ().getBuffValue () + " ";
//		}
//		if (combatant.getPrecision ().getBuffValue () != 0) {
//			buffText.text += "PRECISION " + combatant.getPrecision ().getBuffValue () + " ";
//		}
//		if (combatant.getCritic ().getBuffValue () != 0) {
//			buffText.text += "CRITIC " + combatant.getCritic ().getBuffValue () + " ";
//		}
//		if(combatant.getDodge().getBuffValue() != 0){
//			buffText.text += "DODGE " + combatant.getDodge ().getBuffValue () + " ";
//		}
	}

	public void UseSkillAnimation(){
		animator.SetTrigger ("UseSkill");
	}

	public void playAnimation (IWaitForAnimationByString requester, string trigger){
		animator.SetTrigger (trigger);
		this.requester = requester;
	}

	void finishedAnimationByString(){
		requester.resumeFromAnimation (this);
	}

	public void UseSkillFromCharacterBehavior(){
		combatant.getBehavior ().UseSkill ();
	}

	public void ShowItsActivePlayer(){
		targetButton.ShowItsActivePlayer ();
	}

	public void TurnNameBackToBlack(){
		targetButton.TurnBackToBlack ();
	}

	public void ShowTargetBtn(Character user, Skill skill){
		targetButton.Appear (user,skill);
	}

	public void HideTargetBtn(){
		targetButton.Disappear ();
	}

	public void RefreshInitiative ()
	{
//		DungeonManager dungeonManager = DungeonManager.getInstance ();
//		if (combatant != null) {
//			if (combatant.isAlive ()) {
////				initiative.gameObject.SetActive (true);
////				initiative.text = (dungeonManager.getInitiativeOrder ().IndexOf (combatant) + 1).ToString ();
//				if (initiative.text == "1") {
//					initiative.color = Color.black;
//				} else {
//					initiative.color = Color.gray;
//				}
//			}
//		} else {
//			initiative.gameObject.SetActive(false);
//		}
	}

	public RectTransform getRectTransform(){
		return rectTransform;
	}	
	public Animator getAnimator(){
		return animator;
	}

	public void ShowPossibleInitiative (Character activeCharacter, Skill skill)
	{
		DungeonManager dungeonManager = DungeonManager.getInstance();

	}

//	public void changeEquipmentSprite (Sprite sprite){
//		this.equipment.sprite = sprite;
//	}

	public void changeEquipObject (RectTransform backEquip, RectTransform frontEquip){
		foreach(RectTransform child in frontHandler){
			Destroy (child.gameObject);
		}
		foreach(RectTransform child in backHandler){
			Destroy (child.gameObject);
		}
		if(frontEquip != null){
			frontEquip.SetParent (frontHandler);
			frontEquip.anchoredPosition = new Vector2 (0f,100f);
		}
		if(backEquip != null){
			backEquip.SetParent (backHandler);
			backEquip.anchoredPosition = new Vector2 (0f,100f);
		}
	}

//	public void CheckCharacterThatDied (Character character){
//		if(this.combatant ==){
//
//		}
//	}

//	void OnEnable(){
//		EventManager.OnShowTargetsOf += ShowPossibleInitiative;
//		EventManager.OnClickedTargetBtn += RefreshInitiative;
//		EventManager.OnUnchoosedSkill += RefreshInitiative;
//	}

	public void showTargetBtnWithColor(Color color){
		targetButton.show(color);
	}

	void OnDisable(){
//		EventManager.OnShowTargetsOf -= ShowPossibleInitiative;
//		EventManager.OnClickedTargetBtn -= RefreshInitiative;
//		EventManager.OnUnchoosedSkill -= RefreshInitiative;
		if(combatant!=null){
			combatant.OnHUDValuesChange -= Refresh;
		}
	}

	public Character getCharacter(){return combatant;}
}
