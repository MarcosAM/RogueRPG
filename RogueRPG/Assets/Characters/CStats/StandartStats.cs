using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Character/Stats")]
public class StandartStats : ScriptableObject {
	[SerializeField]string name;
	[SerializeField]private int atk, atkm, def, defm, hp;
	[SerializeField]private float stamina;
	[SerializeField]private Equip[] skills;
	[SerializeField]private Equip momentumSkill;
	[SerializeField]private Image portrait;

	public string getName() {return name;}
	public int getAtk() {return atk;}
	public int getAtkm() {return atkm;}
	public int getDef() {return def;}
	public int getDefm() {return defm;}
	public int getHp() {return hp;}
	public float getStamina() {return stamina;}
	public Equip[] GetEquips() {return skills;}
	public Equip getMomentumSkill() {return momentumSkill;}
	public Image getPortrait () {return portrait;}
}