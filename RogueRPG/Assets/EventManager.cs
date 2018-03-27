using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	public static event Action OnSetSkills;

	public static event Action OnClickedSkillBtn;
	public static event Action OnClickedTargetBtn;

	public static Skill selectedSkill;

	public delegate void ChoosedSkill(Combatant c);
	static ChoosedSkill choosedSkill;

	public static Combatant target;

	public static void SetSkills(){
		if (OnSetSkills != null)
			OnSetSkills ();
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
