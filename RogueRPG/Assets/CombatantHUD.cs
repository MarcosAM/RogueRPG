using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantHUD : MonoBehaviour {

	private Combatant combatant;
	private Slider hpBar;
	[SerializeField]private Text myName;
	[SerializeField]private Text hpNumbers;
	[SerializeField]private TargetBtn targetButton;

	void Awake (){
		hpBar = GetComponentInChildren<Slider>();
		gameObject.SetActive(false);
	}

	public void Initialize (Combatant c)
	{
		gameObject.SetActive(true);
		if (c != null) {
			combatant = c;
			Refresh(combatant);
			myName.text = c.getName ();
			if(targetButton != null)
				targetButton.Initialize(c);
		} else {
			gameObject.SetActive(false);
		}
	}

	public void setHpBar (float v){
		if(v >= 0 || v <= 1)
			hpBar.value = v;
	}

	public void setHpNumbers (float hp, float maxHp){
		hpNumbers.text = hp +"/"+maxHp;
	}

	public void Refresh (Combatant c){
		if(combatant == c){
			setHpBar (c.getHp () / c.getMaxHp ());
			setHpNumbers (c.getHp (), c.getMaxHp ());
		}
	}

	void OnEnable (){
		EventManager.OnCombatantsHpChanging += Refresh;
	}

	void OnDisable (){
		EventManager.OnCombatantsHpChanging -= Refresh;
	}

}
