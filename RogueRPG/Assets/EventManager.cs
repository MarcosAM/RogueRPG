using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	public static event Action OnClickedSkillBtn;
	public static event Action OnClickedTargetBtn;

	public static void ChooseSkill() {if (OnClickedSkillBtn != null) OnClickedSkillBtn();}
	public static void ChooseTarget() {if (OnClickedTargetBtn != null) OnClickedTargetBtn();}
}
