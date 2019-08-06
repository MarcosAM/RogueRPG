using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    static LootManager lootManager;

    [SerializeField] Equip[] allLootOptions;

    [SerializeField] Equip[] warriorInitialEquips;
    [SerializeField] Equip[] mageInitialEquips;
    [SerializeField] Equip[] rogueInitialEquips;

    List<Equip> playerEquip = new List<Equip>();

    int currentIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        lootManager = this;

        RestartProgress();
    }

    void RestartProgress()
    {
        currentIndex = 0;

        playerEquip.Clear();
        foreach (Equip equip in warriorInitialEquips)
        {
            playerEquip.Add(equip);
        }
        foreach (Equip equip in mageInitialEquips)
        {
            playerEquip.Add(equip);
        }
        foreach (Equip equip in rogueInitialEquips)
        {
            playerEquip.Add(equip);
        }

        PartyManager.SetPartyEquips(warriorInitialEquips, mageInitialEquips, rogueInitialEquips);
    }

    public static Equip[] GetLootOptionsAndAdvanceTrack(int characterCount)
    {
        print(lootManager.allLootOptions.Length);
        Equip[] lootOptions = new Equip[characterCount * 2];

        for (int i = 0; i < lootOptions.Length; i++)
        {
            if (lootManager.allLootOptions.Length > lootManager.currentIndex)
            {
                lootOptions[i] = lootManager.allLootOptions[lootManager.currentIndex];
                lootManager.currentIndex++;
            }
            else
            {
                lootOptions[i] = lootManager.allLootOptions[0];
            }
        }

        return lootOptions;
    }

    public static Equip[] GetAllPlayersEquips() { return lootManager.playerEquip.ToArray(); }

    public static Equip[] GetWarriorInitialEquips() { return lootManager.warriorInitialEquips; }
    public static Equip[] GetMageInitialEquips() { return lootManager.mageInitialEquips; }
    public static Equip[] GetRogueInitialEquips() { return lootManager.rogueInitialEquips; }

    public static void AddEquip(Equip equip)
    {
        print(equip.GetEquipName());
        lootManager.playerEquip.Add(equip);
    }
}