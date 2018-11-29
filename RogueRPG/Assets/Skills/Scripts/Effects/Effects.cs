using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effects : ScriptableObject
{
    //TODO Renomear Effects para Boon e fazer com que Boon e Damage herdem de uma classe em comum, provavelmente effects
    public abstract void Affect(Character user, Character target);
    public abstract int SortBestTargets(Character user, Character c1, Character c2);
    //TODO ponderar colocar isso em attack tbm
    //TODO Só para eu não me esquecer, considerar dodge e precisão também nos attacks....Isso vai ser hard!
    public abstract bool IsLogicalTarget(Tile tile);
    public abstract int GetComparableValue(Character character);
}