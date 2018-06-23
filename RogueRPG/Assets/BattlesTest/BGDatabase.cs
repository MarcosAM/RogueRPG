using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDatabase : MonoBehaviour {

	static BGDatabase instance = null;
	[SerializeField]List<BattleGroup> florestBGs;
	void Awake (){
		MakeItASingleton();
	}

	void MakeItASingleton(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public static BGDatabase getInstance (){return instance;}
}
