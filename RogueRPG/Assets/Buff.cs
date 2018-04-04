using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {

	public enum BuffType {Precision, Dodge};

	private BuffType type;
	private int level, duration;
	private Combatant target;

	public void Initialize(int l, int d, BuffType t, Combatant c){
		level = l;
		duration = d;
		type = t;
		target = c;
		Effect ();
	}

	void Effect (){
		switch(type){
		case BuffType.Precision:
			target.increasePrecision (level);
			break;
		case BuffType.Dodge:
			target.increaseDodge (level);
			break;
		default:
			break;
		}
	}

	void Countdown(){
		duration--;
		if(duration<=0){
			End ();
		}
	}

	void End(){
		Destroy (gameObject);
	}
}