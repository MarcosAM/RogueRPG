using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantHUD : MonoBehaviour {

	private Character combatant;
	[SerializeField]private Slider hpBar;
	[SerializeField]private Slider energyBar;
	[SerializeField]private Text hpNumbers;
	[SerializeField]private TargetBtn targetButton;
	[SerializeField]private Image image;
	[SerializeField]private Text buffText;
	[SerializeField]private DamageFB damageFbPrefab;
	private RectTransform rectTransform;

	void Awake(){
		image = GetComponentInChildren<Image> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	public void Initialize (Character c)
	{
		if (c != null) {
			combatant = c;
			image.sprite = combatant.getPortrait ().sprite;
			combatant.OnHUDValuesChange += Refresh;
			combatant.OnHPValuesChange += HPFeedback;
			combatant.OnBuffsGainOrLoss += ShowBuffs;
			//TODO Checar se isso aqui não vai cagar tudo com os buffs dos personagens quando eles se moverem
			ShowBuffs (0f,0f,0f);
			Refresh();
			if(targetButton != null)
				targetButton.Initialize(combatant);
		}
	}

	public void Deinitialize(){
		if(combatant != null){
			combatant.OnHUDValuesChange -= Refresh;
			combatant.OnHPValuesChange -= HPFeedback;
			combatant.OnBuffsGainOrLoss -= ShowBuffs;
		}
	}

	public void HPFeedback(int pastHp, int amountChanged){
		DamageFB damageFb = Instantiate (damageFbPrefab);
		damageFb.transform.SetParent (transform.parent,false);
		damageFb.getRectTransform ().localPosition = rectTransform.localPosition + Vector3.right*50;
		damageFb.Initialize (amountChanged);
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

	void ShowBuffs (float dodge, float precision, float critic){
		buffText.text = "";
		if(dodge>0f){
			buffText.text += "DODGE ";
		}
		if(precision>0f){
			buffText.text += "PRECISION ";
		}
		if(critic>0f){
			buffText.text += "CRITIC ";
		}
	}

	void OnDisable (){
		if(combatant!=null){
			combatant.OnHUDValuesChange -= Refresh;
		}
	}

	public RectTransform getRectTransform(){
		return rectTransform;
	}	
}
