﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombHUDManager : MonoBehaviour {

//	[SerializeField]CombatantHUD combatantHUDprefab;

	[SerializeField]CombatantHUD[] heroesCombatantHUD = new CombatantHUD[4];
	[SerializeField]CombatantHUD[] enemiesCombatantHUD = new CombatantHUD[4];

	[SerializeField] private Vector2[] heroesPositions = new Vector2[4];
	[SerializeField] private Vector2[] enemiesPositions = new Vector2[4];

	//TESTE
	[SerializeField]SkillBtn[] skillsBtn = new SkillBtn[4];
	static CombHUDManager instance = null;
	bool showingMomentumSkill = true;
	[SerializeField]MomentumTabBtn momentumTabBtn;
	[SerializeField]UndoBtn undoBtn;
	[SerializeField]Text chooseYourTxt;

	public void Awake(){
		MakeItASingleton ();
//		chooseYourTxt.gameObject.SetActive(false);
//		DontDestroyOnLoad (this.gameObject);
//		CreateCombatantHUDs ();
	}

//	public void CreateCombatantHUDs(){
//		for(int i = 0; i<4;i++){
//			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
//			combatantHUD.transform.SetParent(transform,false);
//			combatantHUD.getRectTransform().localPosition = heroesPositions[i];
//			heroesCombatantHUD [i] = combatantHUD;
//			heroesCombatantHUD [i].gameObject.SetActive(false);
//		}
//		for(int i = 0; i<4;i++){
//			CombatantHUD combatantHUD = Instantiate(combatantHUDprefab);
//			combatantHUD.transform.SetParent(transform,false);
//			combatantHUD.getRectTransform().localPosition = enemiesPositions[i];
//			enemiesCombatantHUD [i] = combatantHUD;
//			enemiesCombatantHUD [i].gameObject.SetActive (false);
//		}
//	}

//	public void ShowCharacterAt(Character character, int position){
//		if (character.isPlayable ()) {
//			heroesCombatantHUD [position].gameObject.SetActive (true);
//			heroesCombatantHUD [position].Initialize (character);
//			character.setHUD (heroesCombatantHUD [position]);
//		} else {
//			enemiesCombatantHUD [position].gameObject.SetActive (true);
//			enemiesCombatantHUD [position].Initialize (character);
//			character.setHUD (enemiesCombatantHUD [position]);
//		}
//	}

//	public void ShowCombatants (List<Character> heroesParty, List<Character> enemiesParty){
//		ClearCombatantsHUDs ();
//		for(int i = 0;i<heroesParty.Count;i++){
//			if(heroesParty[i]!=null){
//				heroesCombatantHUD [i].gameObject.SetActive (true);
//				heroesCombatantHUD [i].Initialize(heroesParty[i]);
//				heroesParty [i].setHUD (heroesCombatantHUD[i]);
//			}
//		}
//		for(int i = 0;i<enemiesParty.Count;i++){
//			if(enemiesParty[i]!=null){
//				enemiesCombatantHUD [i].gameObject.SetActive (true);
//				enemiesCombatantHUD [i].Initialize(enemiesParty[i]);
//				enemiesParty [i].setHUD (enemiesCombatantHUD[i]);
//			}
//		}
//	}

	public void ShowCombatants (Battleground.Tile[] heroesTiles, Battleground.Tile[] enemiesTiles){
		ClearCombatantsHUDs ();
		for(int i = 0;i<heroesTiles.Length;i++){
			heroesCombatantHUD [i].gameObject.SetActive (true);
			heroesCombatantHUD [i].Initialize(heroesTiles[i]);
			if(heroesTiles[i].getOccupant()!=null)
				heroesTiles[i].getOccupant().setHUD (heroesCombatantHUD[i]);
		}
		for(int i = 0;i<enemiesTiles.Length;i++){
			enemiesCombatantHUD [i].gameObject.SetActive (true);
			enemiesCombatantHUD [i].Initialize(enemiesTiles[i]);
			if(enemiesTiles[i].getOccupant()!=null)
				enemiesTiles[i].getOccupant().setHUD (enemiesCombatantHUD[i]);
		}
	}

	public void ClearCombatantsHUDs(){
		foreach (CombatantHUD combatantHUD in heroesCombatantHUD) {
			combatantHUD.Deinitialize();
			combatantHUD.gameObject.SetActive (false);
		}

		foreach (CombatantHUD combatantHUD in enemiesCombatantHUD) {
			combatantHUD.Deinitialize();
			combatantHUD.gameObject.SetActive (false);
		}
	}

	//TESTE
	public void ShowSkillsBtnOf (Character character)
	{
		chooseYourTxt.gameObject.SetActive(true);
		chooseYourTxt.text = "Choose Your Equipment:";
		if (DungeonManager.getInstance ().isMomentumFull ()) {
			if (showingMomentumSkill) {
				for (int i = 0; i < skillsBtn.Length; i++) {
					skillsBtn [i].showMomentumSkillOf (character);
				}
				momentumTabBtn.appear ("Skills");
			} else {
				for (int i = 0; i < skillsBtn.Length; i++) {
					skillsBtn [i].RefreshSelf (character);
				}
				momentumTabBtn.appear ("Momentum");
			}
		} else {
			for (int i = 0; i < skillsBtn.Length; i++) {
				skillsBtn [i].RefreshSelf (character);
			}
		}
		Character[] allCharacters = FindObjectsOfType<Character> ();
		for (int i = 0; i < allCharacters.Length; i++) {
			allCharacters[i].getHUD().getAnimator().SetBool ("Destaque", false);
		}
		character.getHUD().getAnimator().SetBool ("Destaque", true);
	}

	public void HideSkillsBtn ()
	{
		for (int i = 0; i < skillsBtn.Length; i++) {
			skillsBtn [i].Disappear ();
		}
		chooseYourTxt.gameObject.SetActive(false);
		momentumTabBtn.disappear ();
	}

	void MakeItASingleton(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void RefreshInitiativeHUD (){
		foreach(CombatantHUD hud in heroesCombatantHUD){
			hud.RefreshInitiative();
		}
		foreach(CombatantHUD hud in enemiesCombatantHUD){
			hud.RefreshInitiative();
		}
	}

	public void ShowTargetBtns (Character user, Equip choosedSkill, bool asPreview)
	{
		for (int i = 0; i < heroesCombatantHUD.Length; i++) {
			heroesCombatantHUD [i].ShowTargetBtn (user, choosedSkill);
		}
		for (int i = 0; i < enemiesCombatantHUD.Length; i++) {
			enemiesCombatantHUD [i].ShowTargetBtn (user, choosedSkill);
		}
//		if (!asPreview) {
////			FindObjectOfType<Narration>().Appear(choosedSkill.getMySkillEffectsDescriptions());
//			undoBtn.Appear ();
//			chooseYourTxt.gameObject.SetActive(true);
//			chooseYourTxt.text = "Choose Your Target:";
//		}
	}

	public void HideTargetBtns(bool asPreview){
		for(int i=0; i<heroesCombatantHUD.Length; i++){
			heroesCombatantHUD [i].HideTargetBtn ();
		}
		for(int i=0; i<enemiesCombatantHUD.Length; i++){
			enemiesCombatantHUD [i].HideTargetBtn ();
		}
//		if(!asPreview){
//			chooseYourTxt.gameObject.SetActive(false);
//		}
//		FindObjectOfType<Narration>().Disappear();
//		undoBtn.Disappear ();
	}

	public void onSkillBtnPressed (SkillBtn skillBtn){
//		CombatBehavior combatBehavior = DungeonManager.getInstance ().getInitiativeOrder () [0].getBehavior ();
//		combatBehavior.skillBtnPressed(skillBtn.getSkill());
	}

	public void onTargetBtnPressed (Battleground.Tile targetTile){
//		CombatBehavior combatBehavior = DungeonManager.getInstance ().getInitiativeOrder () [0].getBehavior ();
//		combatBehavior.targetBtnPressed(targetTile);
		FindObjectOfType<PlayerInputManager>().ReturnEquipAndTarget(targetTile);
	}

	public void onSkillBtnHoverEnter (SkillBtn skillBtn)
	{
		//CombatBehavior combatBehavior = DungeonManager.getInstance ().getInitiativeOrder () [0].getBehavior ();
		//if (combatBehavior.getChoosedSkill () != null) {
			
		//} else {
		//	if (skillBtn.getSkill ().getCharactersThatCantUseMe ().Contains (combatBehavior.getCharacter ())) {

		//	} else {
		//		ShowTargetBtns (combatBehavior.getCharacter(),skillBtn.getSkill(), true);
		//	}
		//}
	}

	public void onSkillBtnHoverExit(SkillBtn skillBtn){
		CombatBehavior combatBehavior = DungeonManager.getInstance ().getInitiativeOrder () [0].getBehavior ();
		if (combatBehavior.GetChoosedEquip () != null) {

		} else {
			HideTargetBtns (true);
		}
	}

	public void onTargetBtnHoverEnter(TargetBtn targetBtn)
	{
		FindObjectOfType<PlayerInputManager>().HoverTargetBtnEnter(targetBtn);
	}

	public void PreviewTargets(Character user, Equip selectedEquip ,Battleground.Tile target)
	{
		for (int i = 0; i < heroesCombatantHUD.Length; i++)
		{
			heroesCombatantHUD [i].CheckIfAffected (target, selectedEquip, user);
		}
		for (int i = 0; i < enemiesCombatantHUD.Length; i++)
		{
			enemiesCombatantHUD [i].CheckIfAffected (target, selectedEquip, user);
		}
	}

	public void onTargetBtnHoverExit(TargetBtn targetBtn){
		FindObjectOfType<PlayerInputManager>().HoverTargetBtnExit(targetBtn);
	}

	public void startTurnOf (Character character)
	{
		foreach (CombatantHUD combatantHUD in heroesCombatantHUD) {
			if (combatantHUD.getCharacter () == character) {
				combatantHUD.showTargetBtnWithColor (Color.grey);
			} else {
				combatantHUD.HideTargetBtn();
			}
		}
		foreach (CombatantHUD combatantHUD in enemiesCombatantHUD) {
			if (combatantHUD.getCharacter () == character) {
				combatantHUD.showTargetBtnWithColor (Color.grey);
			} else {
				combatantHUD.HideTargetBtn();
			}
		}
	}

	public void endTurnOf (Character character){
		character.getHUD().getAnimator().SetBool ("Destaque", false);
	}

	public static CombHUDManager getInstance(){return instance;}
	public Vector2[] getHeroesPositions() {return heroesPositions;}
	public Vector2[] getEnemiesPositions() {return enemiesPositions;}

	public void onMomentumTabBtnPressed(){
		if (showingMomentumSkill) {
			showingMomentumSkill = false;
		} else {
			showingMomentumSkill = true;
		}
		ShowSkillsBtnOf(DungeonManager.getInstance().getInitiativeOrder()[0]);
	}

	public CombatantHUD[] getHeroesCombatantHUD() {return heroesCombatantHUD;}
	public CombatantHUD[] getEnemiesCombatantHUD() {return enemiesCombatantHUD;}

//	public class momentumTabBtn{
//		Text text;
//		Button button;
//		CombHUDManager combHudManager;
//
//		public void appear(string buttonText){
//			text.text = buttonText;
//			button.interactable = true;
//		}
//
//		public void disappear(){
//			button.interactable = false;
//		}
//
//		public void onClick(){
//			combHudManager.onMomentumTabBtnPressed ();
//		}
//
//		public momentumTabBtn(Button button, CombHUDManager combHudManager){
//			this.button = button;
//			this.combHudManager = combHudManager;
//			this.text = button.GetComponentInChildren<Text>();
//			this.button.onClick.AddListener(onClick);
//		}
//	}
}