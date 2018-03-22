using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

	SkillBtn[] skillBtns;
	TargetBtn[] targetBtns;

	void Start () {
		skillBtns = FindObjectsOfType<SkillBtn> ();
		targetBtns = FindObjectsOfType<TargetBtn> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
