using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFB : MonoBehaviour {

	RectTransform rectTransform;
	Text text;

	void Awake(){
		rectTransform = GetComponent<RectTransform> ();
		text = GetComponentInChildren<Text> ();
	}

	public void Initialize (int value){
		text.text = value.ToString ();
	}

	public void End (){
		Destroy (gameObject);
	}

	public RectTransform getRectTransform(){
		return rectTransform;
	}
}
