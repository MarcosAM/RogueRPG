using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	public static event Action<Combatant> OnShowSkillsOf;
	public static event Action<Skill> OnPlayerChoosedSkill;
	public static event Action OnClickedSkillBtn;
	public static event Action<Combatant> OnPlayerChoosedTarget;
	public static event Action OnClickedTargetBtn;
	public static event Action OnShowTargetsOf;
	public static event Action OnSkillUsed;
	public static event Action OnEndedTurn;
	public static event Action<Combatant> OnCombatantsHpChanging;
	public static event Action<float> OnRechargeEnergy;
	public static event Action<Combatant> OnRechargedEnergy;
	public static event Action<Combatant> OnDeathOf;

	public static void ShowSkillsOf(Combatant c){
		if (OnShowSkillsOf != null)
			OnShowSkillsOf (c);
	}

	public static void ClickedSkillBtn(Skill s) {
		if (OnClickedSkillBtn != null)
			OnClickedSkillBtn();
		if(OnPlayerChoosedSkill != null)
			OnPlayerChoosedSkill (s);
	}

	public static void ShowTargetsOf(){
		if (OnShowTargetsOf != null)
			OnShowTargetsOf ();
	}

	public static void ChooseTarget(Combatant c) {
		if (OnClickedTargetBtn != null)
			OnClickedTargetBtn();
		if(OnPlayerChoosedTarget != null)
			OnPlayerChoosedTarget(c);
	}

	public static void SkillUsed(){
		if (OnSkillUsed != null)
			OnSkillUsed();
	}

	public static void EndedTurn(){
		if (OnEndedTurn != null)
			OnEndedTurn ();
	}

	public static void RefresHpBarOf (Combatant c){
		if(OnCombatantsHpChanging != null)
			OnCombatantsHpChanging(c);
	}

	public static void RechargeEnergy (float amount){
		if(OnRechargeEnergy != null){
			OnRechargeEnergy(amount);
		}
	}

	public static void RechargedEnergy (Combatant c){
		if(OnRechargedEnergy != null){
			OnRechargedEnergy(c);
		}
	}

	public static void DeathOf(Combatant c){
		if(OnDeathOf != null){
			OnDeathOf (c);
		}
	}
}
