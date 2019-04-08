using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour
{
    //UIParticleSystem uIParticleSystem;
    //ParticleSystem.MainModule main;
    //public Character Owner { get; set; }
    //public Attribute.Type Stats { get; set; }

    //void Awake()
    //{
    //    main = GetComponent<ParticleSystem>().main;
    //    uIParticleSystem = GetComponent<UIParticleSystem>();
    //    transform.SetParent(FindObjectOfType<Canvas>().transform, false);
    //    Stop();
    //}

    //public void PlayAt(Character character, int intensity, Vector2 position, Sprite sprite, Attribute.Type stats)
    //{
    //    if (intensity % 2 == 0)
    //    {
    //        transform.localScale = Vector3.one;
    //        main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 0.573F, 0));
    //    }
    //    else
    //    {
    //        transform.localScale = new Vector3(1, -1, 1);
    //        main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.808f, 0.141F, 0.769f));
    //    }
    //    if (intensity <= 2)
    //    {
    //        main.startSpeed = new ParticleSystem.MinMaxCurve(20);
    //    }
    //    else
    //    {
    //        main.startSpeed = new ParticleSystem.MinMaxCurve(40);
    //    }
    //    if (intensity < 5)
    //    {
    //        main.startSize = new ParticleSystem.MinMaxCurve(12, 25);
    //    }
    //    else
    //    {
    //        main.startSize = new ParticleSystem.MinMaxCurve(25, 30);
    //    }
    //    Owner = character;
    //    Stats = stats;
    //    uIParticleSystem.particleSprite = sprite;
    //    transform.SetParent(character.transform);
    //    transform.localPosition = Vector3.zero;
    //    gameObject.SetActive(true);
    //}

    //public void Stop()
    //{
    //    Owner = null;
    //    gameObject.SetActive(false);
    //}
}