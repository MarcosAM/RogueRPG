using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : CombatBehavior {

	public override void StartTurn ()
	{
		base.StartTurn ();
		character.StartTurn();
		choosedSkill = null;
		targetTile = null;
//		checkForNextStep();
//		ChooseSkill ();
		FindObjectOfType<PlayerInputManager>().showUIFor(this);

	}

	public void checkForNextStep ()
	{
		if (choosedSkill == null) {
//			CombHUDManager.getInstance ().ShowSkillsBtnOf (character);
//			CombHUDManager.getInstance().HideTargetBtns(true);
			FindObjectOfType<EquipToggleManager>().showEquipTogglesFor(character, false);
		} else {
			CombHUDManager.getInstance ().HideSkillsBtn ();
			if (targetTile == null) {
				CombHUDManager.getInstance ().ShowTargetBtns (character, choosedSkill, false);
			} else {
//				CombHUDManager.getInstance().HideTargetBtns();
//				UseSkillAnimation();
				CombHUDManager.getInstance().HideTargetBtns(false);
				UseSkill();
			}
		}
	}

	public override void skillBtnPressed (Skill skill)
	{
		base.skillBtnPressed (skill);
		choosedSkill = skill;
//		checkForNextStep();
	}

	public override void skillBtnPressed (int index)
	{
		base.skillBtnPressed (index);
		choosedSkill = character.getSkills () [index];
		checkForNextStep ();
	}

	public override void targetBtnPressed (Battleground.Tile targetTile)
	{
		base.targetBtnPressed (targetTile);
		this.targetTile = targetTile;
		checkForNextStep();
	}

//	public void ChooseSkill ()
//	{
////		print (character.getName() + " veja suas skills!");
////		EventManager.OnPlayerChoosedSkill += ReadySkill;
////		CombHUDManager.getInstance().ShowSkillsBtnOf(character);
//	}

//	public void ReadySkill (Skill skill)
//	{
//		choosedSkill = skill;
//		EventManager.OnPlayerChoosedSkill -= ReadySkill;
//		EventManager.OnUnchoosedSkill += UnchooseSkill;
//		ChooseTarget ();
//	}

	public override void unchooseSkill ()
	{
		base.unchooseSkill ();
		choosedSkill = null;
		checkForNextStep ();
	}

//	public void UnchooseSkill(){
//		EventManager.OnPlayerChoosedTarget2 -= ReadyTarget;
//		EventManager.OnUnchoosedSkill -= UnchooseSkill;
//		choosedSkill = null;
//		ChooseSkill ();
//	}

	public void ChooseTarget (){
		EventManager.OnPlayerChoosedTarget2 += ReadyTarget;

		//TODO ALTERAR ISSO AQUI LOLOLOLOLO
		EventManager.ShowTargetsOf(character,choosedSkill);
	}

	public void ReadyTarget (Battleground.Tile tile){
//		EventManager.OnPlayerChoosedTarget2 -= ReadyTarget;
//		EventManager.OnUnchoosedSkill -= UnchooseSkill;
		this.targetTile = tile;
		character.getHUD().UseSkillAnimation();
	}

	public void UseSkillAnimation (){
//		EventManager.OnPlayerChoosedTarget2 -= ReadyTarget;
//		EventManager.OnUnchoosedSkill -= UnchooseSkill;
		character.getHUD().UseSkillAnimation();
	}

	public override void UseSkill ()
	{
		base.UseSkill ();
//		EventManager.OnSkillUsed += EndTurn;
//		print (character.getName() + " usa skill brother");
		choosedSkill.UseEquipmentOn (character,targetTile, this);
	}

	public override void useEquip (int equip, Battleground.Tile target)
	{
		base.useEquip (equip, target);
		character.getSkills () [equip].UseEquipmentOn (character, target, this);
	}

	public override void resumeFromEquipment (){
		EndTurn ();
	}
		
	void EndTurn(){
//		EventManager.OnSkillUsed -= EndTurn;
		choosedSkill = null;
		character.EndTurn ();
	}
}
