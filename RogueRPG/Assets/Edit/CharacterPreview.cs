﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    //TODO Se eu inventar de alterar os chapeus baseado na força da galera, eu vou ter que fazer isso aqui tbm
    Animator animator;
    Archetypes.Archetype archetype = Archetypes.Archetype.None;

    [SerializeField] protected RectTransform frontHandler;
    [SerializeField] protected RectTransform backHandler;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchEquip(Equip equip)
    {
        foreach (RectTransform child in frontHandler)
        {
            Destroy(child.gameObject);
        }
        foreach (RectTransform child in backHandler)
        {
            Destroy(child.gameObject);
        }

        if (equip.GetBackEquipPrefab() != null)
        {
            RectTransform backEquip = Instantiate(equip.GetBackEquipPrefab());

            backEquip.SetParent(backHandler);
            backEquip.anchoredPosition = new Vector2(0, 0);
            backEquip.localEulerAngles = Vector3.zero;
        }
        if (equip.GetFrontEquipPrefab() != null)
        {
            RectTransform frontEquip = Instantiate(equip.GetFrontEquipPrefab());

            frontEquip.SetParent(frontHandler);
            frontEquip.anchoredPosition = new Vector2(0, 0);
            frontEquip.localEulerAngles = Vector3.zero;
        }
    }

    public void ChangeEquipObject(Equip equip)
    {
        animator.SetTrigger("ChangeEquip");

        SwitchEquip(equip);
    }

    public void CheckIfShouldChangeArchetype(Equip[] equips)
    {
        if (archetype != Archetypes.GetArchetype(equips))
        {
            SetArchetype(Archetypes.GetArchetype(equips));
        }
    }

    public void SetArchetype(Archetypes.Archetype archetype)
    {
        this.archetype = archetype;

        animator.runtimeAnimatorController = Archetypes.GetAnimator(archetype);
    }
}