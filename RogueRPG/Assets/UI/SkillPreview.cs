using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPreview : MonoBehaviour {

	[SerializeField]private Image circle;
	[SerializeField]private Text text;

	public Image getCircle(){
		return circle;
	}
	public Text getText(){
		return text;
	}
}