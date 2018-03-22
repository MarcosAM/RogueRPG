using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour {

	public float hp = 10;
	public bool playable = false;
	

	public void Attack (Combatant c){
		c.hp -= 1;
		print (c.hp);
	}

	public void SuperAttack (Combatant c){
		c.hp -= 2;
		print (c.hp);
	}
}
