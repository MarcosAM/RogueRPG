using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EquipDatabase : MonoBehaviour
{
    //TODO deletar essa classe
    [SerializeField] Equip[] equipOptionsFor2Char;
    [SerializeField] Equip[] equipOptionsFor3Char;
    List<Equip[]> equipOptions;

    static readonly int optionsPerChar = 2;
    public static readonly int whenThirdCharEnters = 6;

    static EquipDatabase instace;
    /*
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

    //public static Equip[] GetAllEquips() { return instace.equipOptionsFor2Char; }

    void PrepareEquipOptions()
    {
        equipOptions = new List<Equip[]>();

        int max = equipOptionsFor2Char.Length / 4;
        for (int i = 0; i < max; i++)
        {
            Equip[] options = new Equip[4];
            equipOptions.Add(options);
        }
    }
    */
}