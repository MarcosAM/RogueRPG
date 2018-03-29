using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour {

	protected string myName;
	protected float hp = 10;
	protected Skill choosedSkill;
	protected Skill[] skills = new Skill[4];

	public virtual void StartTurn(){
	}

	public virtual void ChooseSkill(){
	}

	public virtual void ReadySkill(Skill s){
	}

	public virtual void ChooseTarget(){
	}

	public virtual void ReadyTarget(Combatant c){
	}

	public virtual void UseSkill(Combatant u, Combatant t){
	}

	public void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		EventManager.EndedTurn ();
	}

	public void Attack (Combatant c, float attack){
		c.TakeDamage(attack);
	}

	public void TakeDamage (float damage)
	{
		hp -= damage;
		print (this.name+" levou "+damage+" de dano! Esta com "+hp+" restantes.");
	}

	public Skill[] getSkills(){
		return skills;
	}
}
