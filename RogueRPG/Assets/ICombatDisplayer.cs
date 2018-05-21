using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatDisplayer {
	void ShowCombatants (Combatant[] heroesParty, Combatant[] enemiesParty);
}
