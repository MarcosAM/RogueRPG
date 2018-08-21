using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject {
	protected int value;
	protected SkillAnimation animation;
	public virtual void effect (Character target){}

	public int getValue () {return value;}
	public SkillAnimation getSkillAnimation () {return animation;}
}