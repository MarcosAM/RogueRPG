using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour {

	[SerializeField]protected string characterName;
	protected int hp;
	protected int maxHp;
	protected float delayCountdown = 0;
	protected bool alive = true;

	//TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
	[SerializeField]protected StandartStats stats;
	protected Stat atk, atkm, def, defm, precision, dodge, critic;
	protected CombatBehavior combatBehavior;
	[SerializeField]protected IMovable movement;

	protected bool playable;

	public Skill[] skills = new Skill[4];

	[SerializeField]protected Image portrait;
	protected CombatantHUD hud;

	public event Action OnHUDValuesChange;
	public event Action<int,int> OnHPValuesChange;
	public event Action OnMyTurnStarts;
	public event Action OnMyTurnEnds;
	public event Action<float,float,float> OnBuffsGainOrLoss;

	public void StartTurn(){
		if(OnMyTurnStarts != null){
			OnMyTurnStarts ();
		}
		SpendBuffs();
	}

	public void EndTurn(){
		if(OnMyTurnEnds!=null){
			OnMyTurnEnds ();
		}
		EventManager.EndedTurn ();
	}

	public void Attack (Character target, float attack, Skill skill){
		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
			distanceInfluenceOnPrecision = -1f;
		}
		if (skill.getCriticRate () + critic.getValue() >= UnityEngine.Random.value) {
			target.TakeDamage (Mathf.RoundToInt((attack + atk.getValue()) * 1.5f));
		} else {
			if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue() >= UnityEngine.Random.value) {
				target.TakeDamage (Mathf.RoundToInt((attack+atk.getValue())*UnityEngine.Random.Range(1f,1.2f)-target.getDefValue()));
			} else {
				print(target.characterName+" se esquivou!");
			}
		}
	}

	public void AttackMagic (Character target, float attack, Skill skill)
	{
		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
			distanceInfluenceOnPrecision = -1f;
		}
		if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue () >= UnityEngine.Random.value) {
			int damage = Mathf.RoundToInt((attack + atkm.getValue()) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefmValue ());
			target.TakeDamage (damage);
		}
	}

	public void TakeDamage (int damage)
	{
		if (damage > 0) {
			if (OnHPValuesChange != null) {
				OnHPValuesChange (hp,damage);
			}
			hp -= damage;
			if(hp<=0){
				Die ();
			}
		}
		RefreshHUD();
	}

	public void Heal(int value){
		if(value>=0 && alive){
			if (OnHPValuesChange != null) {
				OnHPValuesChange (hp,value);
			}
			hp += value;
			if(hp>maxHp){
				hp = maxHp;
			}
		}
		RefreshHUD();
	}

	public void Die(){
		hp = 0;
		delayCountdown = 0;
		alive = false;
		EventManager.DeathOf (this);
		RemoveAllBuffs ();
	}

	public void DelayBy (float amount){
		delayCountdown -= amount;
		RefreshHUD();
	}

	public void RecoverFromDelayBy (float amount){
		if(alive){
			delayCountdown += amount;
			RefreshHUD ();
		}
	}
	public bool IsDelayed(){
		if (delayCountdown >= 0) {
			return false;
		} else {
			return true;
		}
	}

	void RefreshHUD (){
		if(OnHUDValuesChange != null){
			OnHUDValuesChange();
		}
	}

	protected void FillStats (){
		atk.setStatBase (stats.getAtk ());
		atkm.setStatBase (stats.getAtkm ());
		def.setStatBase (stats.getDef ());
		defm.setStatBase (stats.getDefm ());
		skills = stats.getSkills ();
		maxHp = stats.getHp ();
		hp = maxHp;
	}
		
	public void PrepareForFirstBattle (){
		RemoveAllBuffs ();
		delayCountdown = 0;
	}

	public void SpendBuffs (){
		atk.SpendAndCheckIfEnded();
		atkm.SpendAndCheckIfEnded();
		def.SpendAndCheckIfEnded();
		defm.SpendAndCheckIfEnded();
		critic.SpendAndCheckIfEnded();
		dodge.SpendAndCheckIfEnded();
		precision.SpendAndCheckIfEnded();
		if(OnBuffsGainOrLoss!=null){
			OnBuffsGainOrLoss (dodge.getBuffValue(),precision.getBuffValue(),critic.getBuffValue());
		}
	}

	void RemoveAllBuffs(){
		atk.ResetBuff();
		atkm.ResetBuff();
		def.ResetBuff();
		defm.ResetBuff();
		dodge.ResetBuff();
		precision.ResetBuff();
		critic.ResetBuff();
		if(OnBuffsGainOrLoss != null){
			OnBuffsGainOrLoss (dodge.getBuffValue(),precision.getBuffValue(),critic.getBuffValue());
		}
	}

	public void CriticBuff (float buffValue, int buffDuration){
		critic.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss (dodge.getBuffValue(),precision.getBuffValue(),critic.getBuffValue());
	}
	public void DodgeBuff (float buffValue, int buffDuration){
		dodge.BuffIt(buffValue, buffDuration);
		OnBuffsGainOrLoss (dodge.getBuffValue(),precision.getBuffValue(),critic.getBuffValue());
	}
	public void PrecisionBuff (float buffValue, int buffDuration){
		precision.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss (dodge.getBuffValue(),precision.getBuffValue(),critic.getBuffValue());
	}

	public void setStats(StandartStats standartStats){
		this.stats = standartStats;
		FillStats ();
	}

	public Skill[] getSkills() {return skills;}
	public float getHp() {return hp;}
	public float getEnergy() {return delayCountdown;}
	public float getMaxHp() {return maxHp;}
	public string getName() {return characterName;}

	public float getAtkValue() {return atk.getValue();}
	public float getAtkmValue() {return atkm.getValue();}
	public float getDefValue() {return def.getValue();}
	public float getDefmValue() {return defm.getValue();}
	public float getDodgeValue() {return dodge.getValue();}

	public Stat getAtk (){return atk;}
	public Stat getDef (){return def;}
	public Stat getDefm (){return defm;}
	public Stat getAtkm (){return atkm;}
	public Stat getCritic (){return critic;}
	public Stat getPrecision (){return precision;}
	public Stat getDodge (){return dodge;}

	public bool isPlayable() {return playable;}
	public bool isAlive() {return alive;}
	public Image getPortrait() {return portrait;}

	public void setHUD(CombatantHUD combatantHUD) {hud = combatantHUD;}
	public CombatantHUD getHUD() {return hud;}
	public CombatBehavior getBehavior() {return combatBehavior;}
	public IMovable getMovement() {return movement;}
	public int getPosition() {return movement.getPosition ();}
}