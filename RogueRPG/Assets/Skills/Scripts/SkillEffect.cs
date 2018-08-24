using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject, IWaitForAnimationByString {

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
	[SerializeField]protected string description;
	[SerializeField]protected float momentumValue;
	protected int howManyTargets;
	protected int targetsHited;
	protected Character user;
	protected Battleground.Tile targetTile;
	protected IWaitForSkill requester;

//	public virtual void Effect(Character user, Battleground.Tile targetTile) {}

	public void startEffect(Character user, Battleground.Tile tile, IWaitForSkill requester){
		this.requester = requester;
		this.user = user;
		this.targetTile = tile;
		user.getHUD ().playAnimation (this, "UseSkill");
	}

	public void resumeFromSkill(){
		requester.resumeFromSkill ();
	}

	public void resumeFromAnimation (IPlayAnimationByString animationByString){
		Effect ();
	}

	void Effect(){
		if(user.isPlayable())
			DungeonManager.getInstance ().addMomentum (momentumValue);
		if (singleTarget) {
			FindObjectOfType<Narration> ().Appear (user.getName (), effectName);
			EffectAnimation (targetTile);
			UniqueEffect(user,targetTile);
			//TODO a habilidade paia que é utilizada quando não tem o que soltar
			return;
		} else {
			FindObjectOfType<Narration>().Appear(user.getName(), effectName);
			Battleground.Tile[] targets;
			if (targetTile.getOccupant ().isPlayable () == user.isPlayable ()) {
				targets = DungeonManager.getInstance ().getBattleground ().getMySideTiles (user.isPlayable ());
			} else {
				targets = DungeonManager.getInstance ().getBattleground ().getMyEnemiesTiles (user.isPlayable ());
			}

			howManyTargets = targets.Length;
			targetsHited = 0;
			foreach(Battleground.Tile t in targets){
				EffectAnimation(t);
				if (targetTile.getOccupant ().isPlayable () == user.isPlayable ()) {
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
		skillAnimation.PlayAnimation (this,tile);
	}

	public void EndSkill(){
		if (singleTarget) {
			FindObjectOfType<Narration>().Disappear();
			resumeFromSkill ();
		} else {
			targetsHited++;
			if(targetsHited>=howManyTargets){
				FindObjectOfType<Narration>().Disappear();
				resumeFromSkill ();
			}
		}
	}

	protected float getNewHitValue(Character user, Battleground.Tile targetTile){
		return precision + user.getPrecision().getValue() - getDistanceInfluenceOnPrecision(user,targetTile);
	}

	protected int getNewDamage(Character user, int skillDmg){
		if (source == Sources.Physical) {
			return Mathf.RoundToInt(skillDmg + user.getAtkValue () * Random.Range (1f, 1.2f));
		} else {
			return Mathf.RoundToInt(skillDmg + user.getAtkmValue () * Random.Range (1f, 1.2f));
		}
	}

	public float getDistanceInfluenceOnPrecision (Character user, Battleground.Tile targetTile){
		if (type == Skill.Types.Melee) {
			return 0f;
		} else {
			float distanceInfluenceOnPrecision = (range - Mathf.Abs (user.getPosition() - targetTile.getIndex())) * 0.1f;
			if (distanceInfluenceOnPrecision > 0) {
				return 0;
			} else {
				return distanceInfluenceOnPrecision;
			}
		}
	}

//	public virtual void UniqueEffect (Character user, Skill skill, Battleground.Tile tile) {}
	public string getDescription () {return description;}
	public Sources getSource() {return source;}
	public bool canTargetTile() {return canHitTile;}
	public virtual void UniqueEffect (Character user, Battleground.Tile tile) {}
	public virtual void onHitEffect (Character user, Battleground.Tile tile) {}
	public virtual void onMissedEffect (Character user, Battleground.Tile tile) {}
	public string getEffectName (){return effectName;}
	public int getRange(){return range;}
	public Skill.Types getSkillType(){return type;}
	public Kind getKind(){return kind;}
	public float getCritic(){return critic;}
	public float getPrecision(){return precision;}
}
