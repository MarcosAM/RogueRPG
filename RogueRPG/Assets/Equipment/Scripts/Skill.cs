using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

	public enum Targets{Self, Allies, Enemies, All, Location};
	public enum Types{Melee, Ranged};

//	protected Character user;
	protected List<Character> charactersThatCantUseMe = new List<Character>();
	[SerializeField]protected string sName;
//	[SerializeField]protected int sValue;
//	[SerializeField]protected float sDelay;
//	[SerializeField]protected float sPrecision;
//	[SerializeField]protected float sCriticRate;
//	[SerializeField]protected Types sType;
//	[Range(0,3)]
//	[SerializeField]protected int sRange;
//	[SerializeField]protected SkillAnimation sAnimationPrefab;
//	[SerializeField]protected bool sSingleTarget;
//	[SerializeField]protected Targets sTargets;
	[SerializeField]protected SkillEffect meleeEffect;
	[SerializeField]protected SkillEffect rangedEffect;
	[SerializeField]protected SkillEffect selfEffect;
	[SerializeField]protected SkillEffect alliesEffect;
	[SerializeField]protected int hp, atk, atkm, def, defm;
	[SerializeField]protected Types type;
//	protected int howManyTargets;
//	protected int targetsHited;
//	protected bool endable;

	public void UseEquipmentOn (Character user,Battleground.Tile tile){
//		TODO Check if already there before adding
		charactersThatCantUseMe.Add(user);
//		endable = true;
//		user.DelayBy (sDelay);
//		if (sSingleTarget) {
//			FindObjectOfType<Narration> ().Appear (user.getName (), sName);
//			EffectAnimation (tile);
			//TODO a habilidade paia que é utilizada quando não tem o que soltar
			if (user.isPlayable() != tile.isFromHero ()) {
				if (user.isPlayable () != tile.getOccupant ().isPlayable ()) {
					if (Mathf.Abs (tile.getIndex () - user.getPosition ()) <= meleeEffect.getRange ()) {
						meleeEffect.startEffect (user, tile);
						//					Debug.Log ("Na rodada " + DungeonManager.getInstance().getRound() + " " + user.getName()+" usou efeito primário!");
						return;
					} else {
						rangedEffect.startEffect (user, tile);
						return;
					}
				}
			} else {
				if (tile.getOccupant () != null) {
					if (user == tile.getOccupant ()) {
						if (selfEffect != null) {
							selfEffect.startEffect (user, tile);
//							Debug.Log ("Na rodada " + DungeonManager.getInstance().getRound() + " " + user.getName()+" usou efeito secundário!");
							return;
						}
					} else {
						if (alliesEffect != null) {
							alliesEffect.startEffect (user, tile);
//							Debug.Log ("Na rodada " + DungeonManager.getInstance().getRound() + " " + user.getName()+" usou efeito terciário!");
							return;
						}
					}
				} else {
					if (alliesEffect != null) {
						alliesEffect.startEffect (user, tile);
//						Debug.Log ("Na rodada " + DungeonManager.getInstance().getRound() + " " + user.getName()+" usou efeito terciário!");
						return;
					}
				}
			}
//		}
//		else {
//			FindObjectOfType<Narration>().Appear(user.getName(), sName);
//			Battleground.Tile[] targets;
//			if (tile.getOccupant ().isPlayable () == user.isPlayable ()) {
//				targets = DungeonManager.getInstance ().getBattleground ().getMySideTiles (user.isPlayable ());
//			} else {
//				targets = DungeonManager.getInstance ().getBattleground ().getMyEnemiesTiles (user.isPlayable ());
//			}
//
//			howManyTargets = targets.Length;
//			targetsHited = 0;
//			foreach(Battleground.Tile t in targets){
//				EffectAnimation(t);
//				if (tile.getOccupant ().isPlayable () == user.isPlayable ()) {
//					if (tile.getOccupant () == user) {
//						secondaryEffect.UniqueEffect (user, this, t);
//					} else {
//						tertiaryEffect.UniqueEffect (user,this,t);
//					}
//				} else {
//					primaryEffect.UniqueEffect (user,this,t);
//				}
//			}
//		}
	}

//	public void UseEquipmentOn (Character user, Character target){
//		endable = true;
//		FindObjectOfType<Narration>().Appear(user.getName(), sName);
////		EffectAnimation (target);
//		user.DelayBy(sDelay);
//		UniqueEffect(user,target);
//	}

//	public void Effect (Character user, int targetIndex){
//		endable = true;
//		FindObjectOfType<Narration> ().Appear (user.getName(), skillName);
//		user.SpendEnergy (energyCost);
//		Character target;
//		switch(targets){
//		case Targets.Allies:
//			if (user.getIsHero) {
//				target = FindObjectOfType<Battleground> ().getHeroSide () [targetIndex];
//			} else {
//				target = FindObjectOfType<Battleground> ().getEnemySide () [targetIndex];
//			}
//			break;
//		case Targets.Enemies:
//			if (user.getIsHero) {
//				target = FindObjectOfType<Battleground> ().getEnemySide () [targetIndex];
//			} else {
//				target = FindObjectOfType<Battleground> ().getHeroSide () [targetIndex];
//			}
//			break;
//		case 
//		}
//
//		UniqueEffect ();
//	}

//	public void UseEquipmentOn (Character user){
//		endable = true;
//		FindObjectOfType<Narration>().Appear(user.getName(), sName);
//		user.DelayBy(sDelay);
//		List<Character> myTargets = new List<Character>();
//		Character[] temporaryTargets;
//		if(user.isPlayable()){
//			switch(sTargets){
//			case Targets.Allies:
//				temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
//				break;
//			case Targets.Enemies:
//				temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
//				break;
//			case Targets.All:
//				temporaryTargets = FindObjectsOfType<Character> ();
//				break;
//			default:
//				temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
//				break;
//			}
//		}else{
//			switch(sTargets){
//			case Targets.Allies:
//				temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
//				break;
//			case Targets.Enemies:
//				temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
//				break;
//			case Targets.All:
//				temporaryTargets = FindObjectsOfType<Character> ();
//				break;
//			default:
//				temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
//				break;
//			}
//		}
//		for (int i = 0; i<temporaryTargets.Length; i++){
//			if(temporaryTargets[i].isAlive()){
//				myTargets.Add (temporaryTargets[i]);
//			}
//		}
//		howManyTargets = myTargets.Count;
//		targetsHited = 0;
//		foreach (Character target in myTargets) {
////			EffectAnimation(target);
//			UniqueEffect (user, target);
//		}
//	}
//
//	public void EffectAnimation(Battleground.Tile tile){
//		SkillAnimation skillAnimation = Instantiate (sAnimationPrefab);
//		skillAnimation.transform.SetParent (FindObjectOfType<Canvas>().transform,false);
//		skillAnimation.PlayAnimation (this,tile);
//	}
//
//	public void EndSkill(){
//		if (sSingleTarget) {
//			FindObjectOfType<Narration>().Disappear();
//			EventManager.SkillUsed ();
//		} else {
//			targetsHited++;
//			if(targetsHited>=howManyTargets){
//				FindObjectOfType<Narration>().Disappear();
//				EventManager.SkillUsed ();
//			}
//		}
//	}
//
//	public virtual void UniqueEffect (Character user, Character target){
//	}
//
//	public virtual void UniqueEffect (int targetIndex){
//	}
//
	public string getSkillName (){
		return sName;
	}
//
//	public float getPrecision (){
//		return sPrecision;
//	}
//
//	public float getCriticRate (){
//		return sCriticRate;
//	}
//
//	public bool getIsSingleTarget (){
//		return sSingleTarget;
//	}
//
//	public Targets getTargets (){
//		return sTargets;
//	}
//
//	public Types getSkillType(){
//		return sType;
//	}
//
//	public int getRange(){
//		return sRange;
//	}
//
////	public void setUser(Character user){
////		this.user = user;
////	}
////
////	public Character getUser() {return user;}
//
//	public int getValue() {return sValue;}
	public string getMySkillEffectsDescriptions (){
		string descriptions = "";
		descriptions += meleeEffect.getEffectName() + ": " + meleeEffect.getDescription() + "\n";
		descriptions += selfEffect.getEffectName() + ": " + selfEffect.getDescription() + "\n";
		descriptions += alliesEffect.getEffectName() + ": " + alliesEffect.getDescription() + "\n";
		return descriptions;
	}
	
	public List<Character> getCharactersThatCantUseMe () {return charactersThatCantUseMe;}

	public SkillEffect getMeleeEffect() {return meleeEffect;}
	public SkillEffect getSelfEffect() {return selfEffect;}
	public SkillEffect getRangedEffect() {return rangedEffect;}
	public SkillEffect getAlliesEffect() {return alliesEffect;}
	public List<SkillEffect> getAllSkillEffects() {
		List<SkillEffect> allSkillEffects = new List<SkillEffect> ();
		if (meleeEffect != null)
			allSkillEffects.Add (meleeEffect);
		if (selfEffect != null)
			allSkillEffects.Add (selfEffect);
		if (rangedEffect != null)
			allSkillEffects.Add (rangedEffect);
		if (alliesEffect != null)
			allSkillEffects.Add (alliesEffect);
		return allSkillEffects;
	}
	public int getHp() {return hp;}
	public int getAtk() {return atk;}
	public int getAtkm() {return atkm;}
	public int getDef() {return def;}
	public int getDefm() {return defm;}
	public Types getType() {return type;}
}

//			List<Character> myTargets = new List<Character>();
//			Character[] temporaryTargets;
//			if(user.isPlayable()){
//				switch(sTargets){
//				case Targets.Allies:
//					temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
//					break;
//				case Targets.Enemies:
//					temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
//					break;
//				case Targets.All:
//					temporaryTargets = FindObjectsOfType<Character> ();
//					break;
//				default:
//					temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
//					break;
//				}
//			}else{
//				switch(sTargets){
//				case Targets.Allies:
//					temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
//					break;
//				case Targets.Enemies:
//					temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
//					break;
//				case Targets.All:
//					temporaryTargets = FindObjectsOfType<Character> ();
//					break;
//				default:
//					temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
//					break;
//				}
//			}
//			for (int i = 0; i<temporaryTargets.Length; i++){
//				if(temporaryTargets[i].isAlive()){
//					myTargets.Add (temporaryTargets[i]);
//				}
//			}

//			howManyTargets = myTargets.Count;
//			targetsHited = 0;
//			foreach (Character target in myTargets) {
//				EffectAnimation(tile);
//				primaryEffect.Effect(user, this,tile);
//			}