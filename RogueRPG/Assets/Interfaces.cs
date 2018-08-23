using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaySkillAnimation{
	void PlayAnimation (IWaitForAnimation requester, Vector2 animationPosition);
//	void playAnimation (IWaitForAnimation requester);
}

public interface IWaitForAnimation{
	void resumeFromAnimation();
}

public interface IPlayEffects{
	void playEffect (IWaitForEffectsToEnd requester, Vector2 effectPosition);
}

public interface IWaitForEffectsToEnd{
	void resumeFromEffect (IPlayEffects requester);
}

public interface IRegeneratable{
	void startGeneration (int duration);
	void startGeneration ();
}

public interface IPoisonable{
	void getPoisoned ();
}