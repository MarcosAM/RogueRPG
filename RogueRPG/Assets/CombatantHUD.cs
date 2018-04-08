using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantHUD : MonoBehaviour {

	private Combatant combatant;
	[SerializeField]private Slider hpBar;
	[SerializeField]private Slider energyBar;
	[SerializeField]private Text hpNumbers;
	[SerializeField]private TargetBtn targetButton;

	public void Initialize (Combatant c)
	{
		if (c != null) {
			combatant = c;
			combatant.OnHUDValuesChange += Refresh;
			Refresh();
			if(targetButton != null)
				targetButton.Initialize(combatant);
		}
	}

	public void setHpBar (float v){
		if(v >= 0 || v <= 1)
			hpBar.value = v;
	}

	public void setHpNumbers (float hp, float maxHp){
		hpNumbers.text = hp +"/"+maxHp;
	}

	public void Refresh (){
		setHpBar (combatant.getHp () / combatant.getMaxHp ());
		setHpNumbers (combatant.getHp (), combatant.getMaxHp ());
		UpdateEnergyBar();
	}

	void UpdateEnergyBar (){
		float i = (combatant.getEnergy() + 5f) / 5f;
		if (i >= 1) {
			i = 1;
			energyBar.fillRect.GetComponentInChildren<Image> ().color = Color.green;
		} else {
			energyBar.fillRect.GetComponentInChildren<Image>().color = Color.blue;
		}
		energyBar.value = i;
	}

	void OnDisable (){
		combatant.OnHUDValuesChange -= Refresh;
	}

}
