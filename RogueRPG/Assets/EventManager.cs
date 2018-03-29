using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	public static event Action OnClickedSkillBtn;
	public static event Action OnClickedTargetBtn;

	public static Skill selectedSkill;

	public delegate void ChoosedSkill(Combatant c);
	static ChoosedSkill choosedSkill;

	public static Combatant target;

	public static event Action<Combatant> OnHeroTurnStarted;
	public static event Action OnSkillUsed;

	public static void SkillUsed(){
		if (OnSkillUsed != null)
			OnSkillUsed();
	}

	public static void StartHeroTurn(Combatant c){
		if (OnHeroTurnStarted != null)
			OnHeroTurnStarted (c);
	}

	public static void ChooseSkill() {
		if (OnClickedSkillBtn != null)
			OnClickedSkillBtn();
	}

	public static void ChooseTarget() {
		if (OnClickedTargetBtn != null)
			OnClickedTargetBtn();
		//TODO Ajustar esse auto ataque aqui
		selectedSkill.Effect(target,target);
	}
}
