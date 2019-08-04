using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EquipDatabase : MonoBehaviour
{
    //TODO rever tudo isso aqui
    [SerializeField] Equip[] equipOptionsFor2Char;
    [SerializeField] Equip[] equipOptionsFor3Char;
    List<Equip[]> equipOptions;

    static readonly int optionsPerChar = 2;
    public static readonly int whenThirdCharEnters = 6;

    static EquipDatabase instace;

    private void Awake()
    {
        instace = this;
        DontDestroyOnLoad(gameObject);

        foreach (var equip in equipOptionsFor2Char)
        {
            equip.SetHowManyLeft(equip.GetAmount());
        }

        foreach (var attribute in PartyManager.GetParty())
        {
            foreach (var equip in attribute.GetEquips())
            {
                equip.SetHowManyLeft(equip.GetHowManyLeft() - 1);
            }
        }
    }
    /*
    public static Equip UnlockNewEquip(Archetypes.Archetype archetype1, Archetypes.Archetype archetype2, int level1, int level2, int dungeonLevel)
    {
        int lowerLevel = level1 >= level2 ? level2 : level1;
        int sumLevel = level1 + level2;
        if (sumLevel > dungeonLevel)
            sumLevel = dungeonLevel;

        var possibleEquips = instace.equipOptionsFor2Char.Where(e => (e.GetLevel() >= lowerLevel && e.GetLevel() <= sumLevel) && (e.GetArchetype() == archetype1 || e.GetArchetype() == archetype2)).ToArray();

        if (possibleEquips.Length <= 0)
            possibleEquips = instace.equipOptionsFor2Char;

        var randomIndex = Random.Range(0, possibleEquips.Length - 1);
        possibleEquips[randomIndex].SetAmout(possibleEquips[randomIndex].GetAmount() + 1);

        return possibleEquips[randomIndex];
    }
    */

    //public static Equip GetEquip(int equipIndex) { return instace.equipOptionsFor2Char[equipIndex]; }
    public static Equip[] GetAllEquips() { return instace.equipOptionsFor2Char; }

    //public static Equip[] Get

    void PrepareEquipOptions()
    {
        equipOptions = new List<Equip[]>();

        int max = equipOptionsFor2Char.Length / 4;
        for (int i = 0; i < max; i++)
        {
            Equip[] options = new Equip[4];
            for (int l = 0; l < options.Length; l++)
            {
                //options[l]=
            }
            equipOptions.Add(options);
        }
    }
}