using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {

	public enum BuffType {Precision, Dodge, Critic};

	private BuffType type;
	private int level, duration;
	private Character target;

	public void Initialize(int level, int duration, BuffType type, Character target){
		this.level = level;
		this.duration = duration;
		this.type = type;
		this.target = target;
		this.target.CheckNewBuff (this);
	}

	public void Effect (){
		switch(type){
		case BuffType.Precision:
			target.increasePrecision (level);
			break;
		case BuffType.Dodge:
			target.increaseDodge (level);
			break;
		case BuffType.Critic:
			target.increaseCritic (level);
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
		case BuffType.Critic:
			target.resetCritic ();
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