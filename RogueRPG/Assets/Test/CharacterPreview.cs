using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    Animator animator;

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
}
