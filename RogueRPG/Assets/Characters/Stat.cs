using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat {

	float statBase;
	int buffValue;
	int buffDuration;
	List<float> bonus = new List<float>();

	public float getValue (){
		float value;
		value = statBase;
		value += buffValue;
		for(int i=0;bonus.Count;i++){
			value += bonus[i];
		}
		return value;
	}
	public List<float> getBonus() {return bonus;}
}