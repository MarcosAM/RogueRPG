using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject, IWaitForAnimationByString, IWaitForAnimation {

	public enum Sources{Physical, Magic};
	public enum Kind{Offensive, Heal, Buff, Debuff, Movement};

	[SerializeField]protected Sources source;
	[SerializeField]protected float value;
	[SerializeField]protected float precision;
	[SerializeField]protected float critic;
	[SerializeField]protected Skill.Types type;
	[SerializeField]protected Kind kind;
	[SerializeField]protected int range;
	[SerializeField]protected SkillAnimation animationPrefab;
	[SerializeField]protected bool singleTarget;
	[SerializeField]protected string effectName;
	[SerializeField]protected bool canHitTile;
	[SerializeField]protected bool canHitDead;
	[SerializeField]protected string description;
	[SerializeField]protected string castSkillAnimationTrigger;
	[SerializeField]protected float momentumValue;
	protected int howManyTargets;
	protected int targetsHited;
	protected Character user;
	protected Battleground.Tile targetTile;
	protected IWaitForSkill requester;

//	public virtual void Effect(Character user, Battleground.Tile targetTile) {}

	public virtual void startEffect(Character user, Battleground.Tile tile, IWaitForSkill requester){
		this.requester = requester;
		this.user = user;
		this.targetTile = tile;
		playCastSkillAnimation();
	}

	protected void playCastSkillAnimation (){
		if(castSkillAnimationTrigger != null){
			user.getHUD().playAnimation(this,castSkillAnimationTrigger);
		}
	}

	public void endSkill(){
		requester.resumeFromSkill ();
	}

	public virtual void resumeFromAnimation (IPlayAnimationByString animationByString){
		Effect ();
	}

	void Effect(){
		if(user.isPlayable())
			DungeonManager.getInstance ().addMomentum (momentumValue);
		if (singleTarget) {
//			FindObjectOfType<Narration> ().Appear (user.getName (), effectName);
			EffectAnimation (targetTile);
			UniqueEffect(user,targetTile);
			return;
		} else {
//			FindObjectOfType<Narration>().Appear(user.getName(), effectName);
			Debug.Log("Chegou aqui");
			Battleground.Tile[] targets;
			if (targetTile.isFromHero() == user.isPlayable ()) {
				targets = DungeonManager.getInstance ().getBattleground ().getMySideTiles (user.isPlayable ());
			} else {
				targets = DungeonManager.getInstance ().getBattleground ().getMyEnemiesTiles (user.isPlayable ());
			}

			howManyTargets = targets.Length;
			targetsHited = 0;
			foreach(Battleground.Tile t in targets){
				EffectAnimation(t);
				if (targetTile.isFromHero() == user.isPlayable ()) {
					if (targetTile.getOccupant () == user) {
						UniqueEffect (user, t);
					} else {
						UniqueEffect (user,t);
					}
				} else {
					UniqueEffect (user,t);
				}
			}
		}
	}

	public void EffectAnimation(Battleground.Tile tile){
		SkillAnimation skillAnimation = Instantiate (animationPrefab);
		skillAnimation.transform.SetParent (FindObjectOfType<Canvas>().transform,false);
		skillAnimation.PlayAnimation (this,tile.getLocalPosition());
	}

	public void resumeFromAnimation(){
		if (singleTarget) {
//			FindObjectOfType<Narration>().Disappear();
			endSkill ();
		} else {
			targetsHited++;
			if(targetsHited>=howManyTargets){
//				FindObjectOfType<Narration>().Disappear();
				endSkill ();
			}
		}
	}

	protected float getAttack(){
		return Random.value - precision - user.getPrecision().getValue();
	}

	protected bool didIHit(Character target, float attack){
		if (type == Skill.Types.Melee) {
			return target.didIHitYou (attack);
		} else {
			return target.didIHitYou (attack - Mathf.Abs (user.getPosition() - target.getPosition()) );
		}
	}

	protected float getDamage(int skillDamage){
		if (source == Sources.Physical) {
			return (user.getAtkValue () + skillDamage) * Random.Range (1f,1.2f);
		} else {
			return (user.getAtkmValue () + skillDamage) * Random.Range (1f,1.2f);
		}
	}

	protected int damage(Character user, int skillDamage, bool wasCritic){
		return user.takeDamage (skillDamage,source, wasCritic);
	}

	protected bool wasCritic(){
		if (Random.value <= critic + user.getCritic ().getValue () && critic > 0 && source == Sources.Physical) {
			return true;
		} else {
			return false;
		}
	}

	public string getDescription () {return description;}
	public Sources getSource() {return source;}
	public bool canTargetTile() {return canHitTile;}
	public bool canTargetDead (){return canHitDead;}
	public virtual void UniqueEffect (Character user, Battleground.Tile tile) {}
	public virtual void onHitEffect (Character user, Battleground.Tile tile) {}
	public virtual void onMissedEffect (Character user, Battleground.Tile tile) {}
	public string getEffectName (){return effectName;}
	public int getRange(){return range;}
	public Skill.Types getSkillType(){return type;}
	public Kind getKind(){return kind;}
	public float getCritic(){return critic;}
	public float getPrecision(){return precision;}
	public bool isSingleTarget(){return singleTarget;}
}
