using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAnimationManager : MonoBehaviour {

	protected Character user;
	protected Battleground.Tile targetTile;
	protected Skill requester;

	public virtual void start(Skill requester, Battleground.Tile targetTile){}
}