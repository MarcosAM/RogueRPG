﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject {

	[SerializeField]protected int value;

	public virtual void Effect (Character user, Skill skill, Battleground.Tile tile){}
}
