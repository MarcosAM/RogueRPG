using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CombatBtn : MonoBehaviour {

	protected Button button;
	public int number;

	public virtual void Appear(){
	}

	public virtual void Disappear(){
	}

}
