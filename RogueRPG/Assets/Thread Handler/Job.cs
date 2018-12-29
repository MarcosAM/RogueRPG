using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ThreadedJob {

    List<Skill> skills;
    List<TurnSugestion> turnSugestions = new List<TurnSugestion>();
    List<int> probabilities = new List<int>();
    List<Tile> allTiles;

    Equip equip;

    Character user;
    IWaitForEquipment requester;
    bool momentum;

    Battleground battleground;

    protected override void ThreadFunction()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            turnSugestions.Add(skills[i].GetTurnSugestion(user,battleground));
            Debug.Log("Cheguei!");

            for (int j = 0; i < turnSugestions[i].probability; j++)
            {
                probabilities.Add(i);
            }
        }
    }

    protected override void OnFinished()
    {
        // This is executed by the Unity main thread when the job is finished
        var index = probabilities[Random.Range(0, probabilities.Count)];
        equip.UseEquipmentOn(user, allTiles[(int)turnSugestions[index].targetPosition], requester, momentum, index);
    }

    public Job(List<Skill> skills, List<Tile> allTiles, Equip equip, Character user, IWaitForEquipment requester, bool momentum, Battleground battleground)
    {
        this.skills = skills;
        this.allTiles = allTiles;
        this.equip = equip;
        this.user = user;
        this.requester = requester;
        this.momentum = momentum;
        this.battleground = battleground;
    }
}