﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeDontDie : MonoBehaviour {

	public void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}
}
