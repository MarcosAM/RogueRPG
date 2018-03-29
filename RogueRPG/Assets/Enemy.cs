using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant {

	public override void StartTurn ()
	{
		base.StartTurn ();
		EventManager.SkillUsed();
	}
		
}
