using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	public static event Action<Character> OnShowSkillsOf;
	public static event Action<Skill> OnPlayerChoosedSkill;
	public static event Action OnClickedSkillBtn;
	public static event Action<Character> OnPlayerChoosedTarget;
	public static event Action OnClickedTargetBtn;
	public static event Action<Character,Skill> OnShowTargetsOf;
	public static event Action OnUnchoosedSkill;
	public static event Action OnSkillUsed;
	public static event Action OnEndedTurn;
	public static event Action<float> OnRechargeEnergy;
	public static event Action<Character> OnRechargedEnergy;
	public static event Action<Character> OnDeathOf;
//	public static event Action<Party> OnPartyLost;
	public static event Action<int> OnPlayerChoosedLocation;

	public static void ShowSkillsOf(Character c){
		if (OnShowSkillsOf != null)
			OnShowSkillsOf (c);
	}

	public static void ClickedSkillBtn(Skill s) {
		if (OnClickedSkillBtn != null)
			OnClickedSkillBtn();
		if(OnPlayerChoosedSkill != null)
			OnPlayerChoosedSkill (s);
	}

	public static void UnchooseSkill(){
		if(OnUnchoosedSkill != null){
			OnUnchoosedSkill ();
		}
	}

	public static void ShowTargetsOf(Character user, Skill skill){
		if (OnShowTargetsOf != null)
			OnShowTargetsOf (user, skill);
	}

	public static void ChooseTarget(Character c) {
		if (OnClickedTargetBtn != null)
			OnClickedTargetBtn();
		if(OnPlayerChoosedTarget != null)
			OnPlayerChoosedTarget(c);
	}

	public static void ChooseLocation(int position){
		if (OnClickedTargetBtn != null)
			OnClickedTargetBtn();
		if (OnPlayerChoosedLocation != null)
			OnPlayerChoosedLocation (position);
	}

	public static void SkillUsed(){
		if (OnSkillUsed != null)
			OnSkillUsed();
	}

	public static void EndedTurn(){
		if (OnEndedTurn != null)
			OnEndedTurn ();
	}

	public static void RechargeEnergy (float amount){
		if(OnRechargeEnergy != null){
			OnRechargeEnergy(amount);
		}
	}

	public static void RechargedEnergy (Character c){
		if(OnRechargedEnergy != null){
			OnRechargedEnergy(c);
		}
	}

	public static void DeathOf(Character c){
		if(OnDeathOf != null){
			OnDeathOf (c);
		}
	}

//	public static void PartyLost (Party p){
//		if(OnPartyLost != null){
//			OnPartyLost(p);
//		}
//	}
}
