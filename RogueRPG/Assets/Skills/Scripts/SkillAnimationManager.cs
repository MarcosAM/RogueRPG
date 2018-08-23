using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAnimationManager : MonoBehaviour {

	protected Character user;
	protected Battleground.Tile targetTile;
	protected SkillEffect requester;

	public virtual void start(SkillEffect requester, Battleground.Tile targetTile){}
}