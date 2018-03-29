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
}
