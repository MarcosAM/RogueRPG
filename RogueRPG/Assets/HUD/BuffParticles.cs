using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour, IBuffHUD
{
    UIParticleSystem uIParticleSystem;
    [SerializeField] List<Sprite> sprites;

    void Awake()
    {
        uIParticleSystem = GetComponent<UIParticleSystem>();
        transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        Stop();
    }

    public void PlayAt(Stat.Stats stats, Stat.Intensity intensity, Vector2 position)
    {
        uIParticleSystem.particleSprite = sprites[(int)stats];
        transform.localPosition = position;
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}