using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Character/Stats")]
public class StandartStats : ScriptableObject {
	[SerializeField]string name;
	[SerializeField]private int atk, atkm, def, defm, hp;
	[SerializeField]private Skill[] skills;
	[SerializeField]private Image portrait;

	public string getName() {return name;}
	public int getAtk() {return atk;}
	public int getAtkm() {return atkm;}
	public int getDef() {return def;}
	public int getDefm() {return defm;}
	public int getHp() {return hp;}
	public Skill[] getSkills() {return skills;}
	public Image getPortrait () {return portrait;}
}