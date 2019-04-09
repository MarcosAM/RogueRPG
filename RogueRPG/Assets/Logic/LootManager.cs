using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    static LootManager lootManager;

    [SerializeField] Equip[] allLootOptions;
    [SerializeField] Equip[] initialEquips;
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
        foreach (Equip equip in initialEquips)
        {
            playerEquip.Add(equip);
        }
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

    public static void AddEquip(Equip equip)
    {
        lootManager.playerEquip.Add(equip);
    }
}