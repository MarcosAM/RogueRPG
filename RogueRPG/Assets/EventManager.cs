using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	public static event Action OnClickedSkillBtn;
	public static event Action OnClickedTargetBtn;

	public delegate void ChoosedSkill(Combatant c);
	static ChoosedSkill choosedSkill;

	public static Combatant target;

	public static void ChooseSkill(ChoosedSkill d) {
		if (OnClickedSkillBtn != null)
			OnClickedSkillBtn();
		choosedSkill = d;
	}

	public static void ChooseTarget() {
		if (OnClickedTargetBtn != null)
			OnClickedTargetBtn();
		choosedSkill (target);
	}
}
