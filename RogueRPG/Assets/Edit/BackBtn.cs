using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    public void GoBack(int index)
    {
        FindObjectOfType<GameManager>().LoadScene(index);
    }
}
