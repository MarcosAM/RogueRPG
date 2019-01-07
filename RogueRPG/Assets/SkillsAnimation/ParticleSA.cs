using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSA : SkillAnimation
{

    protected ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public override void PlayAnimation(Vector2 animationPosition)
    {
        transform.localPosition = animationPosition;
        particleSystem.Play();
        Destroy(gameObject, particleSystem.main.startLifetime.constantMax);
    }

}
