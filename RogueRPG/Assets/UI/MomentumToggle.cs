using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumToggle : EquipToggle
{
    ParticleSystem particles;

    private void Awake()
    {
        equipToggleManager = FindObjectOfType<EquipToggleManager>();
        FindComponents();
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }

    public override void SetInterectable(bool interactable)
    {
        base.SetInterectable(interactable);
        if (interactable)
            particles.Play();
        else
            particles.Stop();
    }
}