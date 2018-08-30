using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float speed = 10f;
	RectTransform rectTransform;

	void Awake(){
		rectTransform = GetComponent<RectTransform> ();
	}
	
	void Update () {
		rectTransform.Rotate (Vector3.forward, speed * Time.deltaTime);
	}
}