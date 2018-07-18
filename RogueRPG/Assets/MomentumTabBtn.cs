using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomentumTabBtn : MonoBehaviour {
	Text text;
	Button button;

	void Awake(){
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text> ();
		disappear ();
	}

	public void appear (string buttonText){
		text.text = buttonText;
		button.interactable = true;
		text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
	}

	public void disappear(){
		button.interactable = false;
		text.color = new Color (text.color.r,text.color.g,text.color.b,0f);
	}

	public void onClick(){
		CombHUDManager.getInstance ().onMomentumTabBtnPressed ();
	}
}
