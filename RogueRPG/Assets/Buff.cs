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
		target.CheckNewBuff (this);
	}

	public void Effect (){
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

	public bool Countdown ()
	{
		duration--;
		if (duration <= 0) {
			return true;
		} else {
			return false;
		}
	}

	public void End(){
		switch(type){
		case BuffType.Precision:
			target.resetPrecision();
			break;
		case BuffType.Dodge:
			target.resetDodge();
			break;
		default:
			break;
		}
		target.RemoveBuff(this);
	}

	public void Remove(){
		Destroy (gameObject);
	}

	public BuffType getType (){
		return type;
	}

	public int GetLevel (){
		return level;
	}

}