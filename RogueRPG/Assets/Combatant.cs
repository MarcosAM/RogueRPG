using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Combatant : MonoBehaviour {

	[SerializeField]protected string myName;
	protected float hp = 10;
	protected float maxHp = 10;
	protected float energy = 0;

	[SerializeField]protected int atk, atkm, def, defm;
	[SerializeField]protected float dodge = 0;
	[SerializeField]protected float precision = 0;
	[SerializeField]protected float critic = 0;

	protected bool isHero;

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

	public virtual void UseSkill (){
	}

	public void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		EventManager.EndedTurn ();
	}

	public void Attack (Combatant target, float attack, Skill skill)
	{
		if (skill.getCriticRate () + critic >= Random.value) {
			target.TakeDamage ((attack + atk) * 1.2f);
			print (myName + " critou!");
		} else {
			if (skill.getPrecision () + precision - target.getDodge() >= Random.value) {
				target.TakeDamage ((attack+atk)*Random.Range(1f,1.2f)-target.getDef());
			} else {
				print (this.myName + " errou!");
			}
		}
	}

	public void AttackMagic (Combatant target, float attack, Skill skill)
	{
		if (skill.getPrecision () + precision - target.getDodge () >= Random.value) {
			target.TakeDamage ((attack + atkm) * Random.Range (1f, 1.2f) - target.getDefm ());
		} else {
			print (this.myName + " errou!");
		}
	}

	public void TakeDamage (float damage)
	{
		if (damage > 0) {
			hp -= damage;
		} else {
			print (myName + " matou nos peito!");
		}
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

	void OnEnable (){
		EventManager.OnRechargeEnergy += RecoverEnergy;
	}

	void OnDisable (){
		EventManager.OnRechargeEnergy -= RecoverEnergy;
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

	public int getAtk(){
		return atk;
	}

	public int getAtkm (){
		return atkm;
	}

	public int getDef (){
		return def;
	}

	public int getDefm (){
		return defm;
	}

	public float getDodge (){
		return dodge;
	}

	public bool getIsHero (){
		return isHero;
	}
}