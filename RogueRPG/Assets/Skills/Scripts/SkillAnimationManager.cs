using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAnimationManager : MonoBehaviour {

	protected Character user;
	protected Tile targetTile;
	protected Skill requester;

	public virtual void start(Skill requester, Tile targetTile){}
}