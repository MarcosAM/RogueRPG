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
	protected bool cAlive = true;

	//TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
	[SerializeField]protected StandartStats cStats;
	protected Stat atk, atkm, def, defm, precision, dodge, critic;
//	protected int cAtkBase, cAtkmBase, cDefBase, cDefmBase;
//	protected List<int> cAtkBonus = new List<int>();
//	protected List<int> cDefBonus = new List<int>();
//	protected List<int> cAtkmBonus = new List<int>();
//	protected List<int> cDefmBonus = new List<int>();
//	protected List<float> cDodgeBonus = new List<float>();
//	protected List<float> cPrecisionBonus = new List<float>();
//	protected List<float> cCriticBonus = new List<float>();
//
//	[SerializeField]protected float cDodgeBase = 0;
//	[SerializeField]protected float cPrecisionBase = 0;
//	[SerializeField]protected float cCriticBase = 0;
	protected ICombatBehavior cCombatBehavior;
	[SerializeField]protected IMovable cMovement;

	[SerializeField]protected List<Buff> buffs = new List<Buff>();

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
//		SpendBuffs();
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
//		RemoveAllBuffs ();
	}

	public void SpendEnergy (float amount){
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
//		RemoveAllBuffs ();
		cDelayCountdown = 0;
	}

	void OnEnable (){
		EventManager.OnRechargeEnergy += RecoverEnergy;
	}

	void OnDisable (){
		EventManager.OnRechargeEnergy -= RecoverEnergy;
	}

//	public void increasePrecision(int level){
//		float precisionBuff = 0f;
//		switch(level){
//		case 1:
//			precisionBuff = 0.1f;
//			break;
//		case 2:
//			precisionBuff = 0.3f;
//			break;
//		case 3:
//			precisionBuff = 0.5f;
//			break;
//		default:
//			break;
//		}
//		if(cPrecisionBase < precisionBuff){
//			cPrecisionBase = precisionBuff;
//		}
//		if(OnBuffsGainOrLoss != null){
//			OnBuffsGainOrLoss (cDodgeBase,cPrecisionBase,cCriticBase);
//		}
//	}
//
//	public void resetPrecision(){
//		cPrecisionBase = 0;
//		if(OnBuffsGainOrLoss != null){
//			OnBuffsGainOrLoss (cDodgeBase,cPrecisionBase,cCriticBase);
//		}
//	}
//
//	public void increaseDodge(int level){
//		float dodgeBuff = 0f;
//		switch(level){
//		case 1:
//			dodgeBuff = 0.1f;
//			break;
//		case 2:
//			dodgeBuff = 0.3f;
//			break;
//		case 3:
//			cPrecisionBase = 0.5f;
//			break;
//		default:
//			break;
//		}
//		if(cDodgeBase < dodgeBuff){
//			cDodgeBase  = dodgeBuff;
//		}
//		if(OnBuffsGainOrLoss != null){
//			OnBuffsGainOrLoss (cDodgeBase,cPrecisionBase,cCriticBase);
//		}
//	}
//
//	public void resetDodge(){
//		cDodgeBase = 0;
//		if(OnBuffsGainOrLoss != null){
//			OnBuffsGainOrLoss (cDodgeBase,cPrecisionBase,cCriticBase);
//		}
//	}
//
//	public void increaseCritic(int level){
//		float criticBuff = 0f;
//		switch(level){
//		case 1:
//			criticBuff = 0.1f;
//			break;
//		case 2:
//			criticBuff = 0.3f;
//			break;
//		case 3:
//			cPrecisionBase = 0.5f;
//			break;
//		default:
//			break;
//		}
//		if(cCriticBase < criticBuff){
//			cCriticBase  = criticBuff;
//		}
//		if(OnBuffsGainOrLoss != null){
//			OnBuffsGainOrLoss (cDodgeBase,cPrecisionBase,cCriticBase);
//		}
//	}
//
//	public void resetCritic(){
//		cCriticBase = 0;
//		if(OnBuffsGainOrLoss != null){
//			OnBuffsGainOrLoss (cDodgeBase,cPrecisionBase,cCriticBase);
//		}
//	}
//
//	public void CheckNewBuff (Buff newBuff)
//	{
//		foreach (Buff currentBuff in buffs) {
//			if (currentBuff.getType () == newBuff.getType ()) {
//				if (newBuff.GetLevel () >= currentBuff.GetLevel ()) {
//					RemoveBuff (currentBuff);
//					AddNewBuff (newBuff);
//					return;
//				} else {
//					print("Novo buff muito fraco");
//					newBuff.Remove();
//					return;
//				}
//			}
//		}
//		AddNewBuff(newBuff);
//	}
//
//	void AddNewBuff (Buff newBuff){
//		buffs.Add(newBuff);
//		newBuff.Effect();
//	}
//
//	public void RemoveBuff(Buff b){
//		buffs.Remove(b);
//		b.Remove();
//	}
//
//	public void SpendBuffs ()
//	{
//		List<Buff> deletableBuffs = new List<Buff> ();
//		foreach (Buff buff in buffs) {
//			if (buff.Countdown ()) {
//				deletableBuffs.Add(buff);
//			}
//		}
//		foreach(Buff buff in deletableBuffs){
//			buff.End();
//		}
//	}
//
	void RemoveAllBuffs(){
		atk.ResetBuff();
		atkm.ResetBuff();
		def.ResetBuff();
		defm.ResetBuff();
		dodge.ResetBuff();
		precision.ResetBuff();
		critic.ResetBuff();
		OnBuffsGainOrLoss (dodge.getBuffValue(),precision.getBuffValue(),critic.getBuffValue());
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