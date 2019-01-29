using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizePopUp : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void ShowPrizePopUp(Transform frontEquipPrefab, Transform backEquipPrefab, string equipName)
    {
        if (backEquipPrefab)
        {
            Transform backEquip = Instantiate(backEquipPrefab);
            backEquip.SetParent(transform);
            backEquip.localPosition = new Vector2(0, 0);
            backEquip.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
        }

        if (frontEquipPrefab)
        {
            Transform frontEquip = Instantiate(frontEquipPrefab);
            frontEquip.SetParent(transform);
            frontEquip.localPosition = new Vector2(0, 0);
            frontEquip.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
        }
        text.text = equipName;

        gameObject.SetActive(true);
    }
}