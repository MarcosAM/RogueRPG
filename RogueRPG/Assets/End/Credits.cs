using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    Animator animator;
    CreditsListener listener;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Play(CreditsListener listener = null)
    {
        this.listener = listener;
        if (animator)
        {
            animator.SetTrigger("play");
        }
        else
        {
            Stop();
        }
    }

    void Stop()
    {
        if (this.listener != null)
        {
            listener.OnCreditsEnded();
        }
    }

    public interface CreditsListener
    {
        void OnCreditsEnded();
    }
}