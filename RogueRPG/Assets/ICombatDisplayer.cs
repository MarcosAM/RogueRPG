using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatDisplayer {
	void ShowCombatants (Character[] heroesParty, Character[] enemiesParty);
}
