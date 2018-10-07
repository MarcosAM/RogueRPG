using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour, IBuffHUD
{
    UIParticleSystem uIParticleSystem;
    ParticleSystem particleSystem;
    [SerializeField] List<Sprite> spritesBuff;
    [SerializeField] List<Sprite> spritesDebuff;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        uIParticleSystem = GetComponent<UIParticleSystem>();
        transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        Stop();
    }

    public void PlayAt(Stat.Stats stats, Stat.Intensity intensity, Vector2 position)
    {
        if ((int)stats % 2 == 0)
            uIParticleSystem.particleSprite = spritesBuff[(int)stats];
        else
            uIParticleSystem.particleSprite = spritesDebuff[(int)stats];
        var main = particleSystem.main;
        //main.startSpeed = new ParticleSystem.MinMaxCurve(20 + 10 * (int)intensity);
        main.startSize = new ParticleSystem.MinMaxCurve(10 * (int)intensity);
        transform.localPosition = position;
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}