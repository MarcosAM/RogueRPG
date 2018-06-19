using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour {

	[SerializeField]protected string cName;
	protected int cHp;
	protected int cMaxHp;
	protected float cDelayCountdown = 0;
	[SerializeField]protected bool cAlive = true;

	//TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
	[SerializeField]protected StandartStats cStats;
	protected Stat atk, atkm, def, defm, precision, dodge, critic;
	protected ICombatBehavior cCombatBehavior;
	[SerializeField]protected IMovable cMovement;

	protected bool cPlayable;

	public Skill[] cSkills = new Skill[4];

	[SerializeField]protected Image cPortrait;
	protected CombatantHUD cHud;

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
		print (this.cName+" atacou com "+distanceInfluenceOnPrecision+" essa influência graças a distância. Coisa de louco. SO EXCITED!!");
		if (skill.getCriticRate () + critic.getValue() >= UnityEngine.Random.value) {
			target.TakeDamage (Mathf.RoundToInt((attack + atk.getValue()) * 1.5f));
		} else {
			if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue() >= UnityEngine.Random.value) {
				target.TakeDamage (Mathf.RoundToInt((attack+atk.getValue())*UnityEngine.Random.Range(1f,1.2f)-target.getDefValue()));
			} else {
				print(target.cName+" se esquivou!");
			}
		}
	}

	public void AttackMagic (Character target, float attack, Skill skill)
	{
		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
			distanceInfluenceOnPrecision = -1f;
		}
		print (this.cName+" atacou com "+distanceInfluenceOnPrecision+" essa influência graças a distância. Coisa de louco. SO EXCITED!!");
		if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue () >= UnityEngine.Random.value) {
			int damage = Mathf.RoundToInt((attack + atkm.getValue()) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefmValue ());
			print (damage);
			target.TakeDamage (damage);
		}
	}

	public void TakeDamage (int damage)
	{
		if (damage > 0) {
			if (OnHPValuesChange != null) {
				OnHPValuesChange (cHp,damage);
			}
			cHp -= damage;
			if(cHp<=0){
				Die ();
			}
		}
		RefreshHUD();
	}

	public void Heal(int value){
		if(value>=0 && cAlive){
			if (OnHPValuesChange != null) {
				OnHPValuesChange (cHp,value);
			}
			cHp += value;
			if(cHp>cMaxHp){
				cHp = cMaxHp;
			}
		}
		RefreshHUD();
	}

	public void Die(){
		cHp = 0;
		cDelayCountdown = 0;
		cAlive = false;
		EventManager.DeathOf (this);
		RemoveAllBuffs ();
	}

	public void DelayBy (float amount){
		cDelayCountdown -= amount;
		RefreshHUD();
	}

	public void RecoverEnergy (float amount){
		if(cAlive){
			cDelayCountdown += amount;
			RefreshHUD();
			if(cDelayCountdown>=0){
				EventManager.RechargedEnergy(this);
			}
		}
	}

	void RefreshHUD (){
		if(OnHUDValuesChange != null){
			OnHUDValuesChange();
		}
	}

	protected void FillStats (){
		atk.setStatBase (cStats.getAtk ());
		atkm.setStatBase (cStats.getAtkm ());
		def.setStatBase (cStats.getDef ());
		defm.setStatBase (cStats.getDefm ());
		cSkills = cStats.getSkills ();
		cMaxHp = cStats.getHp ();
		cHp = cMaxHp;
	}
		
	public void PrepareForNextCombat (){
		RemoveAllBuffs ();
		cDelayCountdown = 0;
	}

	void OnEnable (){
		EventManager.OnRechargeEnergy += RecoverEnergy;
	}

	void OnDisable (){
		EventManager.OnRechargeEnergy -= RecoverEnergy;
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
		this.cStats = standartStats;
		FillStats ();
	}

	public Skill[] getSkills() {return cSkills;}
	public float getHp() {return cHp;}
	public float getEnergy() {return cDelayCountdown;}
	public float getMaxHp() {return cMaxHp;}
	public string getName() {return cName;}

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

	public bool isPlayable() {return cPlayable;}
	public bool isAlive() {return cAlive;}
	public Image getPortrait() {return cPortrait;}

	public void setHUD(CombatantHUD combatantHUD) {cHud = combatantHUD;}
	public CombatantHUD getHUD() {return cHud;}
	public ICombatBehavior getBehavior() {return cCombatBehavior;}
	public IMovable getMovement() {return cMovement;}
	public int getPosition() {return cMovement.getPosition ();}
}