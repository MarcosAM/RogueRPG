using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterFactory : ScriptableObject
{
    [SerializeField] protected string cName;
    [SerializeField] protected bool playable;
    [SerializeField] protected Equip[] equips;
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected Character characterPrefab;

    public abstract Character GetCharacter();
}
