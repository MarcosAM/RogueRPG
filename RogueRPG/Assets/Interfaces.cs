using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaySkillAnimation{
	void PlayAnimation (IWaitForAnimation requester, Vector2 animationPosition);
}

public interface IWaitForAnimation{
	void resumeFromAnimation();
}