using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour, IBuffHUD
{
    UIParticleSystem uIParticleSystem;

    void Awake()
    {
        uIParticleSystem = GetComponent<UIParticleSystem>();
        transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        Stop();
    }

    public void PlayAt(Stat.Stats stats, Vector2 position)
    {
        transform.localPosition = position;
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}