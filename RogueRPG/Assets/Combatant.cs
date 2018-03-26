using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour {

	protected float hp = 10;
	public Skill[] skills = new Skill[4];

	public void Attack (Combatant c, float attack){
		c.TakeDamage(attack);
	}

	public void TakeDamage (float damage)
	{
		hp -= damage;
		print (this.name+" levou "+damage+" de dano! Esta com "+hp+" restantes.");
	}
}
