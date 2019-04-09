using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootSceneManager : MonoBehaviour
{
    List<LootOptions> lootOptions = new List<LootOptions>();

    private void Start()
    {
        lootOptions = GetComponentsInChildren<LootOptions>().ToList();

        int characterCount = PartyManager.GetParty().Length;
        var options = LootManager.GetLootOptionsAndAdvanceTrack(characterCount);

        for (int i = 0; i < lootOptions.Count; i++)
        {
            if (i < characterCount)
            {
                lootOptions[i].RefreshOptions(options[i + i], options[i + i + 1]);
            }
            else
            {
                lootOptions[i].gameObject.SetActive(false);
            }
        }
    }

    public void AddSelectedEquips()
    {
        foreach (LootOptions lo in lootOptions)
        {
            if (lo.gameObject.activeSelf)
            {
                LootManager.AddEquip(lo.GetSelectedEquip());
            }
        }
    }
}