using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Character/Stats")]
public class StandartStats : ScriptableObject {
	[SerializeField]string name;
	[SerializeField]private int atk, atkm, def, defm, hp;
	[SerializeField]private float stamina;
	[SerializeField]private Skill[] skills;
	[SerializeField]private Image portrait;
	[SerializeField]private CombatBehavior behaviorPrefab;

	public string getName() {return name;}
	public int getAtk() {return atk;}
	public int getAtkm() {return atkm;}
	public int getDef() {return def;}
	public int getDefm() {return defm;}
	public int getHp() {return hp;}
	public float getStamina() {return stamina;}
	public Skill[] getSkills() {return skills;}
	public Image getPortrait () {return portrait;}
	public CombatBehavior getCombatBehavior (){
		CombatBehavior behavior = Instantiate(behaviorPrefab);
		return behavior;
	}
}