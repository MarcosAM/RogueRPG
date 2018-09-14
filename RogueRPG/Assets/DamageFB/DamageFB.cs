using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFB : MonoBehaviour {

	RectTransform rectTransform;
	Text text;
	Image image;
	[SerializeField]Sprite criticSprite;

	void Awake(){
		rectTransform = GetComponent<RectTransform> ();
		text = GetComponentInChildren<Text> ();
		image = GetComponentInChildren<Image> ();
	}

	public void Initialize (int value, bool wasCritic){
		if(wasCritic){
			image.sprite = criticSprite;
			rectTransform.sizeDelta = new Vector2 (70,70);
		}
		text.text = value.ToString ();
	}

	public void End (){
		Destroy (gameObject);
	}

	public RectTransform getRectTransform(){
		return rectTransform;
	}
}
