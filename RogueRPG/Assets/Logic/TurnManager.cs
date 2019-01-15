using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, IWaitForEquipment
{
    Character currentCharacter;
    bool momentumTurn;
    DungeonManager dungeonManager;
    PlayerInputManager playerInputManager;

    void Awake()
    {
        dungeonManager = GetComponent<DungeonManager>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    public virtual void StartTurn(Character character)
    {
        this.currentCharacter = character;

        character.GetAttributes().SpendBuffs();
        character.GetInventory().CheckIfEquipsShouldBeRefreshed();

        if (!character.GetAttributes().GetHp().IsAlive())
        {
            dungeonManager.StartCoroutine(dungeonManager.NextTurn());
            return;
        }

        if (character.IsPlayable())
            playerInputManager.ShowUIFor(currentCharacter);
        else
        {
            var index = character.GetInventory().GetUsableEquips()[Random.Range(0, character.GetInventory().GetUsableEquips().Count)];
            momentumTurn = character.GetInventory().IsMomentumEquip(index);
            character.GetInventory().GetAvailableEquips()[index] = false;
            character.GetInventory().GetEquips()[index].UseEquipment(character, this, index);
            //TODO tem personagem se movendo além do range
            //TODO ter algum tipo de probabilidade ou algo do tipo para sair do momentum. Talvez ter 4 turnos para usar, mas cada golpe ou coisa parecida tem chance de tirar um turno do tempo do momentum.
        }
    }
    public virtual void UseEquip(int equipIndex, Tile target, int skill)
    {
        momentumTurn = currentCharacter.GetInventory().IsMomentumEquip(equipIndex);
        currentCharacter.GetInventory().GetAvailableEquips()[equipIndex] = false;
        currentCharacter.GetInventory().GetEquips()[equipIndex].UseEquipmentOn(currentCharacter, target, this, skill);
    }

    public virtual void ResumeFromEquipment()
    {
        if (momentumTurn)
            FindObjectOfType<Momentum>().OnMomentumEquipUsed();
        dungeonManager.StartCoroutine(dungeonManager.NextTurn());
        //TODO eu não sei se é muito seguro isso, talvez possa buggar com várias pessoas pedindo para essa courotine começar
    }
}