using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour
{
    UIParticleSystem uIParticleSystem;
    ParticleSystem particleSystem;
    public Character Owner { get; set; }
    public Attribute.Stats Stats { get; set; }

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        uIParticleSystem = GetComponent<UIParticleSystem>();
        transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        Stop();
    }

    public void PlayAt(Character character, int intensity, Vector2 position, Sprite sprite, Attribute.Stats stats)
    {
        Owner = character;
        Stats = stats;
        uIParticleSystem.particleSprite = sprite;
        var main = particleSystem.main;
        main.startSize = new ParticleSystem.MinMaxCurve(10 * intensity);
        //transform.localPosition = position;
        transform.SetParent(character.transform);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        Owner = null;
        gameObject.SetActive(false);
    }
}