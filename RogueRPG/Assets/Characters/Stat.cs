using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat {

	float statBase = 0;
	float buffValue = 0;
	int buffDuration = 0;
	List<float> bonus = new List<float>();

	public float getValue (){
		float value;
		value = statBase;
		value += buffValue;
		for(int i=0;i<bonus.Count;i++){
			value += bonus[i];
		}
		return value;
	}
	public void BuffIt (float buffValue, int buffDuration){
		if(buffValue>this.buffValue){
			this.buffValue = buffValue;
			this.buffDuration = buffDuration;
		}
		if(buffValue == this.buffValue){
			this.buffDuration = buffDuration;
		}
//		TODO atualizar a interface para mostrar esse bonus
	}
	public void setStatBase (float value){
		this.statBase = value;
	}
	public List<float> getBonus() {return bonus;}
	public float getBuffValue () {return buffValue;}
	public void ResetBuff (){
		buffValue = 0;
		buffDuration = 0;
	}
	public void SpendAndCheckIfEnded (){
		if(buffValue!=0){
			buffDuration--;
			if(buffDuration<=0){
				buffValue = 0;
			}
		}
	}

//	public Stat (){
//		this.buffDuration = 0;
//		this.buffValue = 0;
//		this.statBase = 0;
//	}

	public const float CRITIC_BUFF_1 = 0.1f;
	public const float CRITIC_BUFF_2 = 0.3f;
	public const float CRITIC_BUFF_3 = 0.5f;
	public const float DODGE_BUFF_1 = 0.1f;
	public const float DODGE_BUFF_2 = 0.3f;
	public const float DODGE_BUFF_3 = 0.5f;
	public const float PRECISION_BUFF_1 = 0.1f;
	public const float PRECISION_BUFF_2 = 0.3f;
	public const float PRECISION_BUFF_3 = 0.5f;
}