using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Narration : MonoBehaviour {

	Image image;
	Text text;
		
	void Start () {
		image = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
		Disappear();
	}

	public void Appear(string userName, string skillName){
		text.text = userName+" Usou "+skillName+"!";
		Appear();
	}

	void Appear (){
		image.enabled = true;
		text.enabled = true;
	}

	public void Disappear(){
		image.enabled = false;
		text.enabled = false;
	}

}
