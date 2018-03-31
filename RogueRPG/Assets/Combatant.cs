using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Combatant : MonoBehaviour {

	protected string myName;
	protected float hp = 10;
	protected float maxHp = 10;
	protected float energy = 0;
	protected Skill choosedSkill;
	public Skill[] skills = new Skill[4];
	[SerializeField]protected Slider energyBar;

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
		EventManager.RefresHpBarOf(this);
	}

	public void SpendEnergy (float amount){
		energy -= amount;
		UpdateEnergyBar();
	}

	public void RecoverEnergy (float amount){
		energy += amount;
		UpdateEnergyBar();
		if(energy>=0){
			EventManager.RechargedEnergy(this);
		}
	}

	void UpdateEnergyBar ()
	{
		float i = (energy + 5f) / 5f;
		if (i >= 1) {
			i = 1;
			energyBar.fillRect.GetComponentInChildren<Image> ().color = Color.green;
		} else {
			energyBar.fillRect.GetComponentInChildren<Image>().color = Color.blue;
		}
		energyBar.value = i;
	}

	public Skill[] getSkills(){
		return skills;
	}

	public float getHp (){
		return hp;
	}

	public float getMaxHp (){
		return maxHp;
	}

	public string getName (){
		return myName;
	}

	void OnEnable (){
		EventManager.OnRechargeEnergy += RecoverEnergy;
	}

	void OnDisable (){
		EventManager.OnRechargeEnergy -= RecoverEnergy;
	}

}
