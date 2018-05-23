using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatBehavior {
	void Act();
	void Initialize (Character character);
}