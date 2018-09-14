﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Character : MonoBehaviour, IComparable, IRegeneratable, IPoisonable {

	[SerializeField]protected string characterName;
	protected int hp;
	protected int maxHp;
	protected float delayCountdown = 0;
	protected float maxStamina;
	protected float currentStamina;
	[SerializeField]protected bool alive = true;

	//TODO provavelmente é melhor que isso só tenha para NonPlayable Characters
	[SerializeField]protected StandartStats stats;
	protected Stat atk, atkm, def, defm, precision, dodge, critic;
	protected CombatBehavior combatBehavior;
	[SerializeField]protected IMovable movement;

	protected bool playable;

	public Skill[] skills = new Skill[4];
	protected Skill momentumSkill;

	[SerializeField]protected Image portrait;
	protected CombatantHUD hud;
	protected RegenerationAndPoisonManager regenerationManager;

	public event Action OnHUDValuesChange;
	public event Action<int,int,bool> OnHPValuesChange;
	public event Action OnMyTurnStarts;
	public event Action OnMyTurnEnds;
	public event Action OnBuffsGainOrLoss;

	public void StartTurn(){
		if(OnMyTurnStarts != null){
			OnMyTurnStarts ();
		}
//		RecoverFromDelayBy (delayCountdown*-1);
		SpendBuffs();
		hud.ShowItsActivePlayer ();
		CheckIfSkillsShouldBeRefreshed();
		if (regenerationManager != null) {
			regenerationManager.recover ();
		} else {
			
		}
	}

	public void EndTurn(){
		if(OnMyTurnEnds!=null){
			OnMyTurnEnds ();
		}
		hud.TurnNameBackToBlack ();
		EventManager.EndedTurn ();
	}

//	public void Attack (Character target, float attack, Skill skill){
//		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
//		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
//			distanceInfluenceOnPrecision = -1f;
//		}
//		if (skill.getCriticRate () + critic.getValue() >= UnityEngine.Random.value) {
//			target.TakeDamage (Mathf.RoundToInt((attack + atk.getValue()) * 1.5f));
//		} else {
//			if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue() >= UnityEngine.Random.value) {
//				target.TakeDamage (Mathf.RoundToInt((attack+atk.getValue())*UnityEngine.Random.Range(1f,1.2f)-target.getDefValue()));
//			} else {
////				print(target.characterName+" se esquivou!");
//			}
//		}
//	}

//	public void Attack (Character target, float attack, SkillEffect skill){
//		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
//		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
//			distanceInfluenceOnPrecision = -1f;
//		}
//		if (skill.getCritic () + critic.getValue() >= UnityEngine.Random.value) {
//			target.TakeDamage (Mathf.RoundToInt((attack + atk.getValue()) * 1.5f));
//		} else {
//			if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue() >= UnityEngine.Random.value) {
//				target.TakeDamage (Mathf.RoundToInt((attack+atk.getValue())*UnityEngine.Random.Range(1f,1.2f)-target.getDefValue()));
//			} else {
//				//				print(target.characterName+" se esquivou!");
//			}
//		}
//	}

//	public void AttackMagic (Character target, float attack, Skill skill)
//	{
//		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
//		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
//			distanceInfluenceOnPrecision = -1f;
//		}
//		if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue () >= UnityEngine.Random.value) {
//			int damage = Mathf.RoundToInt((attack + atkm.getValue()) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefmValue ());
//			target.TakeDamage (damage);
//		}
//	}

//	public void AttackMagic (Character target, float attack, SkillEffect skill)
//	{
//		float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
//		if(skill.getSkillType() == Skill.Types.Melee && distanceInfluenceOnPrecision < 0){
//			distanceInfluenceOnPrecision = -1f;
//		}
//		if (skill.getPrecision () + precision.getValue() + distanceInfluenceOnPrecision - target.getDodgeValue () >= UnityEngine.Random.value) {
//			int damage = Mathf.RoundToInt((attack + atkm.getValue()) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefmValue ());
//			target.TakeDamage (damage);
//		}
//	}

	public void HitWith (Character target, float attack, SkillEffect skill){
		if (skill.getSource () == SkillEffect.Sources.Physical) {
			if (skill.getCritic () + critic.getValue () >= UnityEngine.Random.value)
				target.loseHpBy (Mathf.RoundToInt ((attack + atk.getValue ()) * 1.5f), true);
			else
				target.loseHpBy (Mathf.RoundToInt ((attack + atk.getValue ()) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefValue ()), false);
		} else {
			target.loseHpBy (Mathf.RoundToInt((attack + atkm.getValue()) * UnityEngine.Random.Range (1f, 1.2f) - target.getDefmValue ()), false);
		}
	}

//	public void TryToHitWith (Character target, SkillEffect skillEffect){
//		if (getPrecisionOfSkillEffect(target, skillEffect) >= UnityEngine.Random.value) {
//			skillEffect.onHitEffect (target, skillEffect);
//		} else {
//			skillEffect.onMissedEffect (target,skillEffect);
//		}
//	}

	public void TryToHitWith (Battleground.Tile target, SkillEffect skillEffect){
		if (getPrecisionOfSkillEffect(target, skillEffect) >= UnityEngine.Random.value) {
			skillEffect.onHitEffect (this, target);
		} else {
			skillEffect.onMissedEffect (this, target);
			if(target.getOccupant() != null)
				target.getOccupant().getHUD().getAnimator().SetTrigger("Dodge");
		}
	}

	public bool didIHitYou (float attackValue){
		if (attackValue + getDodgeValue () < 0) {
			return true;
		} else {
			hud.getAnimator ().SetTrigger ("Dodge");
			return false;
		}
	}

	public bool CanIHitWith (Character target, SkillEffect skillEffect){
		if (skillEffect.getSkillType () == Skill.Types.Ranged) {
//			if (getPrecisionOfSkillEffect (target, skillEffect) > 0) {
				return true;
//			} else
//				return false;
		} else {
			if (Mathf.Abs (target.getPosition () - getPosition ()) <= skillEffect.getRange ()) {
				return true;
			} else {
				return false;
			}
		}
	}

	public bool CanIHitWith (Battleground.Tile target, SkillEffect skillEffect){
		if (skillEffect.canTargetTile ()) {
			if (skillEffect.getSkillType() == Skill.Types.Ranged) {
//				if (getPrecisionOfSkillEffect (target, skillEffect) > 0) {
					return true;
//				} else
//					return false;
			} else {
				if (Mathf.Abs (target.getIndex () - getPosition ()) <= skillEffect.getRange ()) {
					return true;
				} else {
					return false;
				}
			}
		} else {
			if (target.getOccupant () != null) {
				return CanIHitWith (target.getOccupant(), skillEffect);
			} else {
				return false;
			}
		}
	}

	public float getPrecisionOfSkillEffect(Character target, SkillEffect skill){
		if (CanIHitWith (target, skill)) {
//			float distanceInfluenceOnPrecision = (skill.getRange () - Mathf.Abs (getPosition () - target.getPosition ())) * 0.1f;
			return skill.getPrecision () + precision.getValue () + getDistanceInfluenceOnPrecision(target,skill) - target.getDodgeValue ();
		} else {
			return -1;
		}
	}

	public bool didIHitYouWith(float precision){
		if (precision - getDodgeValue () >= 0) {
			return true;
		} else {
			return false;
		}
	}

	public int takeDamage(int damage, SkillEffect.Sources damageSource, bool wasCritic){
		if (damageSource == SkillEffect.Sources.Physical) {
			if (wasCritic) {
				loseHpBy (Mathf.RoundToInt(damage), true);
				return Mathf.RoundToInt(damage);
			} else {
				loseHpBy (Mathf.RoundToInt(damage - getDefValue()), false);
				return Mathf.RoundToInt(damage - getDefValue ());
			}
		} else {
			loseHpBy (Mathf.RoundToInt(damage - getDefmValue()), false);
			return Mathf.RoundToInt(damage - getDefmValue());
		}
	}

	public float getPrecisionOfSkillEffect(Battleground.Tile target, SkillEffect skill){
		if (CanIHitWith (target, skill)) {
			if (target.getOccupant () != null) {
				return getPrecisionOfSkillEffect (target.getOccupant (), skill);
			} else {
				if (skill.canTargetTile ()) {
					return skill.getPrecision() + precision.getValue();
				} else {
					return -1;
				}
			}
		} else {
			return -1;
		}
	}

	public float getDistanceInfluenceOnPrecision(Character target, SkillEffect skill){
		return getDistanceInfluenceOnPrecision (target.getPosition (), skill);
	}

	public float getDistanceInfluenceOnPrecision(Battleground.Tile target, SkillEffect skill){
		return getDistanceInfluenceOnPrecision (target.getIndex (), skill);
	}

	public float getDistanceInfluenceOnPrecision(int targetPosition, SkillEffect skill){
		if (skill.getSkillType () == Skill.Types.Melee) {
			return 0f;
		} else {
			float distanceInfluenceOnPrecision = skill.getRange () - Mathf.Abs (getPosition () - targetPosition) * 0.1f;
			if (distanceInfluenceOnPrecision > 0) {
				return 0;
			} else {
				return distanceInfluenceOnPrecision;
			}
		}
	}

	public void loseHpBy (int damage, bool wasCritic)
	{
		if (damage > 0) {
			if (OnHPValuesChange != null) {
				OnHPValuesChange (hp,damage,wasCritic);
			}
			hp -= damage;
			currentStamina += damage;
			hud.getAnimator().SetTrigger("Damage");
			if(hp<=0){
				Die ();
			}
		}
		RefreshHUD();
	}

	public void Heal(int value){
		if(value>=0 && alive){
			if (OnHPValuesChange != null) {
				OnHPValuesChange (hp,value, false);
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
		regenerationManager.poisened = false;
		regenerationManager.duration = 0;
	}

	public void revive(int hpRecovered){
		alive = true;
		Heal (hpRecovered);
		DungeonManager.getInstance ().addToInitiative (this);
		RefreshHUD ();
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

//	protected void FillStats (){
//		atk.setStatBase (stats.getAtk ());
//		atkm.setStatBase (stats.getAtkm ());
//		def.setStatBase (stats.getDef ());
//		defm.setStatBase (stats.getDefm ());
//		skills = stats.getSkills ();
//		maxHp = stats.getHp ();
//		hp = maxHp;
//	}

	protected virtual void FillStats (){}
		
	public void refresh (){
		RemoveAllBuffs ();
		delayCountdown = 0;
		hp = maxHp;
		regenerationManager.poisened = false;
		regenerationManager.duration = 0;
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
			OnBuffsGainOrLoss ();
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
			OnBuffsGainOrLoss ();
		}
	}

	void CheckIfSkillsShouldBeRefreshed ()
	{
		for(int i=0; i<skills.Length; i++){
			if(!skills[i].getCharactersThatCantUseMe().Contains(this)){
				return;
			}
		}
		for(int i=0; i<skills.Length; i++){
			skills[i].getCharactersThatCantUseMe().Remove(this);
		}
	}

	public void AtkBuff (float buffValue, int buffDuration){
		atk.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss ();
	}
	public void AtkmBuff (float buffValue, int buffDuration){
		atkm.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss ();
	}
	public void DefBuff (float buffValue, int buffDuration){
		def.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss ();
	}
	public void DefmBuff (float buffValue, int buffDuration){
		defm.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss ();
	}
	public void CriticBuff (float buffValue, int buffDuration){
		critic.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss ();
	}
	public void DodgeBuff (float buffValue, int buffDuration){
		dodge.BuffIt(buffValue, buffDuration);
		OnBuffsGainOrLoss ();
	}
	public void PrecisionBuff (float buffValue, int buffDuration){
		precision.BuffIt(buffValue,buffDuration);
		OnBuffsGainOrLoss ();
	}

	public int CompareTo(object obj){
		if(obj == null){
			return 1;
		}

		Character other = obj as Character;
		if(this.delayCountdown - other.getDelayCountdown() == 0){
			return 0;
		}
		if (this.delayCountdown > other.getDelayCountdown ()) {
			return 1;
		} else {
			return -1;
		}
	}

	public void setStats(StandartStats standartStats){
		this.stats = standartStats;
		FillStats ();
	}

	public Skill[] getSkills() {return skills;}
	public Skill getMomentumSkill() {return momentumSkill;}
	public List<Skill> getUsableSkills() {
		List<Skill> usableSkills = new List<Skill> ();
		for(int i=0; i<skills.Length; i++){
			if(!skills[i].getCharactersThatCantUseMe().Contains(this)){
				usableSkills.Add (skills[i]);
			}
		}
		return usableSkills;
	}
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

	public float getDelayCountdown(){return delayCountdown;}
	public float getCurrentStamina(){return currentStamina;}
	public float getMaxStamina(){return maxStamina;}

	public void setName(string name){
		this.characterName = name;
	}
	public void setHUD(CombatantHUD combatantHUD) {hud = combatantHUD;}
	public CombatantHUD getHUD() {return hud;}
	public CombatBehavior getBehavior() {return combatBehavior;}
	public IMovable getMovement() {return movement;}
	public int getPosition() {return movement.getPosition ();}

	protected class RegenerationAndPoisonManager{
		public int duration = 0;
		public bool consumable = true;
		public bool poisened = false;
		Character owner;

		public void recover(){
			if (poisened) {
				owner.loseHpBy (Mathf.RoundToInt(owner.getMaxHp()*0.1f), false);
			} else {
				if(duration > 0 || !consumable){
					owner.Heal (Mathf.RoundToInt(owner.getMaxHp()*0.1f));
					if(consumable){
						duration--;
					}
				}
			}
		}

		public RegenerationAndPoisonManager (Character owner){
			this.owner = owner;
		}
	}

	public void startGeneration (int duration){
		regenerationManager.duration += duration;
		regenerationManager.poisened = false;
	}
	public void startGeneration (){
		regenerationManager.consumable = false;
		regenerationManager.poisened = false;
	}
	public void getPoisoned (){
		regenerationManager.duration = 0;
		regenerationManager.poisened = true;
	}

//	public void changeEquipmentSprite (Sprite sprite){
//		this.hud.changeEquipmentSprite(sprite);
//	}

	public void changeEquipObject (Image backEquip, Image frontEquip){
		this.hud.changeEquipObject (backEquip, frontEquip);
	}
}