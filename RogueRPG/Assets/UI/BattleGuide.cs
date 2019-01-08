using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleGuide : MonoBehaviour
{

    [SerializeField] Text text;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void setAnimatorTrigger(string trigger)
    {
        if (gameObject.activeInHierarchy)
            animator.SetTrigger(trigger);
    }

    public void setText(string text) { this.text.text = text; }
}
