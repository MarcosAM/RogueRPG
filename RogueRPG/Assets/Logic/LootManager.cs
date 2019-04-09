using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    static LootManager lootManager;

    [SerializeField] Equip[] allLootOptions;
    [SerializeField] List<Equip> loot;
    int currentIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        lootManager = this;
    }

    void RestartProgress()
    {
        currentIndex = 0;
    }

    public static Equip[] GetLootOptionsAndAdvanceTrack(int characterCount)
    {
        Equip[] lootOptions = new Equip[characterCount * 2];

        for (int i = 0; i < lootOptions.Length; i++)
        {
            if (lootManager.allLootOptions.Length < lootManager.currentIndex)
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

    public static void AddLoot(Equip equip)
    {
        lootManager.loot.Add(equip);
    }
}