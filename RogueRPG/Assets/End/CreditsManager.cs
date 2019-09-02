using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour, Credits.CreditsListener
{

    Credits credits;

    void Start()
    {
        credits = FindObjectOfType<Credits>();

        if (credits)
        {
            credits.Play(this);
        }
        else
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        GameManager gameManager = GameManager.getInstance();

        if (gameManager)
        {
            gameManager.LoadScene(0);
        }
    }

    public void OnCreditsEnded()
    {
        RestartGame();
    }
}