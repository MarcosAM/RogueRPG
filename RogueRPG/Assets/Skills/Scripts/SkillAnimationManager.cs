using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAnimationManager : MonoBehaviour {

	protected Character user;
	protected Battleground.Tile targetTile;

	public virtual void start(Character user, Battleground.Tile targetTile){}
}