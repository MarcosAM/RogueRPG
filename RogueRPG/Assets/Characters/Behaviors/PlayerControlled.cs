using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : CombatBehavior {

	public override void StartTurn ()
	{
		base.StartTurn ();
		character.StartTurn();
		ChooseSkill ();
	}

	public void ChooseSkill ()
	{
		print (character.getName() + " veja suas skills!");
		EventManager.OnPlayerChoosedSkill += ReadySkill;
		CombHUDManager.getInstance().ShowSkillsBtnOf(character);
	}

	public void ReadySkill (Skill skill)
	{
		choosedSkill = skill;
		EventManager.OnPlayerChoosedSkill -= ReadySkill;
		EventManager.OnUnchoosedSkill += UnchooseSkill;
		ChooseTarget ();
	}

	public void UnchooseSkill(){
		EventManager.OnPlayerChoosedTarget2 -= ReadyTarget;
		EventManager.OnUnchoosedSkill -= UnchooseSkill;
		choosedSkill = null;
		ChooseSkill ();
	}

	public void ChooseTarget (){
		EventManager.OnPlayerChoosedTarget2 += ReadyTarget;

		//TODO ALTERAR ISSO AQUI LOLOLOLOLO
		EventManager.ShowTargetsOf(character,choosedSkill);
	}

	public void ReadyTarget (Battleground.Tile tile){
		EventManager.OnPlayerChoosedTarget2 -= ReadyTarget;
		EventManager.OnUnchoosedSkill -= UnchooseSkill;
		this.targetTile = tile;
		character.getHUD().UseSkillAnimation();
	}

	public override void UseSkill ()
	{
		base.UseSkill ();
		EventManager.OnSkillUsed += EndTurn;
//		print (character.getName() + " usa skill brother");
		choosedSkill.UseEquipmentOn (character,targetTile);
	}

	void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		choosedSkill = null;
		character.EndTurn ();
	}
}
