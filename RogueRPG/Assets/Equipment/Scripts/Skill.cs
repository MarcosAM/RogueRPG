using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : ScriptableObject, IWaitForSkill {

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
	[SerializeField]protected Sprite image;
	[SerializeField]protected Image backEquipPrefab;
	[SerializeField]protected Image frontEquipPrefab;
	protected Image backEquip;
	protected Image frontEquip;
	[SerializeField]protected Types type;
	protected IWaitForEquipment requester;
//	protected int howManyTargets;
//	protected int targetsHited;
//	protected bool endable;

	public void UseEquipmentOn (Character user,Battleground.Tile tile, IWaitForEquipment requester){
//		TODO Check if already there before adding
//		user.changeEquipmentSprite(image);
		user.changeEquipObject (getBackEquip(),getFrontEquip());
		this.requester = requester;
		charactersThatCantUseMe.Add(user);

		if (tile.isFromHero () == user.isPlayable ()) {
			if (tile.getIndex () == user.getPosition ()) {
				selfEffect.startEffect (user,tile,this);
			} else {
				alliesEffect.startEffect (user, tile, this);
			}
		} else {
			if ((Mathf.Abs (tile.getIndex () - user.getPosition ()) <= meleeEffect.getRange ()) && tile.getOccupant () != null) {
				meleeEffect.startEffect (user, tile, this);
			} else {
				rangedEffect.startEffect (user, tile, this);
			}
		}
	}

	public void resumeFromSkill (){
		requester.resumeFromEquipment ();
	}

	public string getSkillName (){
		return sName;
	}

	public string getMySkillEffectsDescriptions (){
		string descriptions = "";
		descriptions += meleeEffect.getEffectName() + ": " + meleeEffect.getDescription() + "\n";
		descriptions += rangedEffect.getEffectName() + ": " + rangedEffect.getDescription() + "\n";
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
	public Sprite getSprite (){return image;}

	public Image getBackEquip (){
		if(backEquipPrefab != null){
			if(backEquip == null){
				backEquip = Instantiate (backEquipPrefab);
			}
			return backEquip;
		} else{
			return null;
		}
	}
	public Image getFrontEquip (){
		if (frontEquipPrefab != null) {
			if(frontEquip == null){
				frontEquip = Instantiate (frontEquipPrefab);
			}
			return frontEquip;
		} else {
			return null;
		}
	}
}