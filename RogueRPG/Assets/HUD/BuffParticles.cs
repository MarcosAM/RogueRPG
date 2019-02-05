using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour
{
    UIParticleSystem uIParticleSystem;
    ParticleSystem.MainModule main;
    public Character Owner { get; set; }
    public Attribute.Type Stats { get; set; }

    void Awake()
    {
        main = GetComponent<ParticleSystem>().main;
        uIParticleSystem = GetComponent<UIParticleSystem>();
        transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        Stop();
    }

    public void PlayAt(Character character, int intensity, Vector2 position, Sprite sprite, Attribute.Type stats, Color minColor, Color maxColor)
    {
        transform.localScale = intensity % 2 == 0 ? Vector3.one : new Vector3(1, -1, 1);
        Owner = character;
        Stats = stats;
        uIParticleSystem.particleSprite = sprite;
        main.startColor = new ParticleSystem.MinMaxGradient(minColor, maxColor);
        //main.startSize = new ParticleSystem.MinMaxCurve(10 * intensity);
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