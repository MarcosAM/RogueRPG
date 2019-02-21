using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Factory/Standart Monster")]
public class StandartMonsterFactory : CharacterFactory
{
    [SerializeField] protected Color color;
    [SerializeField] protected RuntimeAnimatorController runtimeAnimatorController;

    public override Character GetCharacter()
    {
        //TODO testar se return na base function vai estragar tudo!
        var character = Instantiate(characterPrefab);
        character.Initialize();

        character.Playable = false;
        character.GetInventory().SetEquips(character, equips);

        character.GetAnimator().runtimeAnimatorController = runtimeAnimatorController;

        character.Name = cName;
        character.GetAvatarImg().sprite = sprite;
        character.GetAvatarImg().color = color;

        character.GetComponentInChildren<CharacterHUD>().Initialize(character);

        return character;
    }
}
