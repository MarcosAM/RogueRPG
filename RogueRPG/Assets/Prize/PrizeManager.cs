using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeManager : MonoBehaviour
{
    [SerializeField] PrizeSpinner[] prizeSpinners;

    void Start()
    {
        var partySize = PartyManager.GetParty().Length;
        //var dungeonLevel = DungeonManager.GetLastDungeonLevel();

        for (var i = 0; i < prizeSpinners.Length; i++)
        {
            if (i < partySize)
                //prizeSpinners[i].Initialize(i, dungeonLevel);
                prizeSpinners[i].Initialize(i, 2);
            else
                prizeSpinners[i].Hide();
        }
        Destroy(this);
    }
}