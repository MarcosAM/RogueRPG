using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour {

	public void startTutorial(){
		FindObjectOfType<GameManager>().LoadDungeonScene();
	}
}
