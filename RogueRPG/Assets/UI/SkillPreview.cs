using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField]private Image circle;
	[SerializeField]private Text text;
	[SerializeField]private Text description;
	[SerializeField]private Image descriptionBackground;

	public Image getCircle(){
		return circle;
	}
	public Text getText(){
		return text;
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
	}
}