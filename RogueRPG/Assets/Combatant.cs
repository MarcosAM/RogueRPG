﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Combatant : MonoBehaviour {

	[SerializeField]protected string myName;
	protected int hp = 100;
	protected int maxHp = 100;
	protected float energy = 0;
	[SerializeField]protected bool alive = true;

	[SerializeField]protected int atk, atkm, def, defm;
	[SerializeField]protected float dodge = 0;
	[SerializeField]protected float precision = 0;
	[SerializeField]protected float critic = 0;

	[SerializeField]protected List<Buff> buffs = new List<Buff>();

	protected bool isHero;

	protected Skill choosedSkill;
	public Skill[] skills = new Skill[4];

	public event Action OnHUDValuesChange;


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
		if (skill.getCriticRate () + critic >= UnityEngine.Random.value) {
			target.TakeDamage (Mathf.RoundToInt((attack + atk) * 1.2f));
		} else {
			if (skill.getPrecision () + precision - target.getDodge() >= UnityEngine.Random.value) {
				target.TakeDamage (Mathf.RoundToInt((attack+atk)*UnityEngine.Random.Range(1f,1.2f)-target.getDef()));
			} else {
				print(target.myName+" se esquivou!");
			}
		}
	}

	public void AttackMagic (Combatant target, float attack, Skill skill)
	{
		if (skill.getPrecision () + precision - target.getDodge () >= UnityEngine.Random.value) {
			target.TakeDamage (Mathf.RoundToInt((attack + atkm) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefm ()));
		} else {
		}
	}

	public void TakeDamage (int damage)
	{
		if (damage > 0) {
			
			hp -= damage;
			if(hp<=0){
				Die ();
			}
		} else {
		}
		RefreshHUD();
	}

	public void Heal(int value){
		if(value>=0 && alive){
			hp += value;
			if(hp>maxHp){
				hp = maxHp;
			}
		}
		RefreshHUD();
	}

	public void Die(){
		hp = 0;
		energy = 0;
		alive = false;
		EventManager.DeathOf (this);
		print (this.name+" morreu");
	}

	public void SpendEnergy (float amount){
		energy -= amount;
		RefreshHUD();
	}

	public void RecoverEnergy (float amount){
		if(alive){
			energy += amount;
			RefreshHUD();
			if(energy>=0){
				EventManager.RechargedEnergy(this);
			}
		}
	}

	void RefreshHUD (){
		if(OnHUDValuesChange != null){
			OnHUDValuesChange();
		}
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

	public float getEnergy (){
		return energy;
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

	public bool isAlive(){
		return alive;
	}

	public void increasePrecision(int level){
		float precisionBuff = 0f;
		switch(level){
		case 1:
			precisionBuff = 0.1f;
			break;
		case 2:
			precisionBuff = 0.3f;
			break;
		case 3:
			precisionBuff = 0.5f;
			break;
		default:
			break;
		}
		if(precision < precisionBuff){
			precision = precisionBuff;
		}
	}

	public void resetPrecision(){
		precision = 0;
	}

	public void increaseDodge(int level){
		float dodgeBuff = 0f;
		switch(level){
		case 1:
			dodgeBuff = 0.1f;
			break;
		case 2:
			dodgeBuff = 0.3f;
			break;
		case 3:
			precision = 0.5f;
			break;
		default:
			break;
		}
		if(dodge < dodgeBuff){
			dodge  = dodgeBuff;
		}
	}

	public void resetDodge(){
		dodge = 0;
	}

	public void CheckNewBuff (Buff newBuff)
	{
		foreach (Buff currentBuff in buffs) {
			if (currentBuff.getType () == newBuff.getType ()) {
				if (newBuff.GetLevel () >= currentBuff.GetLevel ()) {
					RemoveBuff (currentBuff);
					AddNewBuff (newBuff);
					return;
				} else {
					print("Novo buff muito fraco");
					newBuff.Remove();
					return;
				}
			}
		}
		AddNewBuff(newBuff);
	}

	void AddNewBuff (Buff newBuff){
		buffs.Add(newBuff);
		newBuff.Effect();
	}

	public void RemoveBuff(Buff b){
		buffs.Remove(b);
		b.Remove();
	}

	protected void SpendBuffs ()
	{
		List<Buff> deletableBuffs = new List<Buff> ();
		foreach (Buff buff in buffs) {
			if (buff.Countdown ()) {
				deletableBuffs.Add(buff);
			}
		}
		foreach(Buff buff in deletableBuffs){
			buff.End();
		}
	}
}