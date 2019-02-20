using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character Factory/Hero")]
public class HeroFactory : CharacterFactory
{
    public override Character GetCharacter()
    {
        var character = Instantiate(characterPrefab);
        character.Initialize();

        character.Playable = playable;
        character.GetInventory().SetEquips(character, equips);

        character.GetAnimator().runtimeAnimatorController = Archetypes.GetAnimator(character.GetInventory().Archetype);

        character.SetName(cName);
        character.GetAvatarImg().sprite = sprite;

        var hat = Archetypes.GetHat(character.GetInventory().Archetype);
        hat.SetParent(character.GetAvatarImg().rectTransform);
        hat.localPosition = Vector2.zero;

        character.GetComponentInChildren<CharacterHUD>().Initialize(character);

        return character;
    }

    public string GetName() { return cName; }
    public void SetName(string name) { cName = name; }

    public Equip[] GetEquips() { return equips; }
}