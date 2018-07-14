﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject {

	public enum Sources{Physical, Magic};

	[SerializeField]protected Sources source;
	[SerializeField]protected int value;
	[SerializeField]protected float precision;
	[SerializeField]protected float critic;
	[SerializeField]protected Skill.Types type;
	[SerializeField]protected int range;
	[SerializeField]protected SkillAnimation animationPrefab;
	[SerializeField]protected bool singleTarget;
	[SerializeField]protected string effectName;
	[SerializeField]protected bool canHitTile;
	[SerializeField]protected string description;
	protected int howManyTargets;
	protected int targetsHited;
//	protected bool endable;

	public void Effect(Character user, Battleground.Tile tile){
		if (singleTarget) {
			FindObjectOfType<Narration> ().Appear (user.getName (), effectName);
			EffectAnimation (tile);
			UniqueEffect(user,tile);
			//TODO a habilidade paia que é utilizada quando não tem o que soltar
			return;
		} else {
			FindObjectOfType<Narration>().Appear(user.getName(), effectName);
			Battleground.Tile[] targets;
			if (tile.getOccupant ().isPlayable () == user.isPlayable ()) {
				targets = DungeonManager.getInstance ().getBattleground ().getMySideTiles (user.isPlayable ());
			} else {
				targets = DungeonManager.getInstance ().getBattleground ().getMyEnemiesTiles (user.isPlayable ());
			}

			howManyTargets = targets.Length;
			targetsHited = 0;
			foreach(Battleground.Tile t in targets){
				EffectAnimation(t);
				if (tile.getOccupant ().isPlayable () == user.isPlayable ()) {
					if (tile.getOccupant () == user) {
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
			EventManager.SkillUsed ();
		} else {
			targetsHited++;
			if(targetsHited>=howManyTargets){
				FindObjectOfType<Narration>().Disappear();
				EventManager.SkillUsed ();
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
	public float getCritic(){return critic;}
	public float getPrecision(){return precision;}
}
