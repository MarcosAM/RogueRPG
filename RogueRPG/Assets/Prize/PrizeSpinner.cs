using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PrizeSpinner : MonoBehaviour
{
    Equip prize;
    [SerializeField] RectTransform[] stops;
    float[] currentAngles;
    float[] targetAngles;
    [SerializeField] Transform[] spinners;
    Button button;
    //bool spinning;
    [SerializeField] Text warningText;
    [SerializeField] string warning;
    [SerializeField] PrizePopUp prizePopUp;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        warningText.transform.parent.gameObject.SetActive(false);
        Initialize(0, 2);
    }

    public void Initialize(int charIndex, int dungeonLevel)
    {
        //TODO alterar para ter equipamentos de nível 0 mesmo 
        var validEquips = PartyManager.GetParty()[charIndex].GetEquips().Where(e => e.GetLevel() <= dungeonLevel).ToArray();

        UpdateSlotsTexts(validEquips);

        if (validEquips.Length <= 1)
            BlockSelf();

        var random1 = Random.Range(0, validEquips.Length - 1);
        var random2 = Random.Range(0, validEquips.Length - 1);

        prize = EquipDatabase.UnlockNewEquip(validEquips[random1].GetArchetype(), validEquips[random2].GetArchetype(), validEquips[random1].GetLevel(), validEquips[random2].GetLevel(), dungeonLevel);

        PrepareSpinners(new int[] { random1, random2 });
    }

    void PrepareSpinners(int[] stopsIndex)
    {
        currentAngles = new float[spinners.Length];
        targetAngles = new float[spinners.Length];

        for (var i = 0; i < spinners.Length; i++)
        {
            currentAngles[i] = spinners[i].rotation.eulerAngles.z;
            var direction = stops[stopsIndex[i]].position - spinners[i].position;
            targetAngles[i] = (Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg) * 55;
            if (targetAngles[i] == 0)
                targetAngles[i] = 360 * 55;
        }
    }

    void UpdateSlotsTexts(Equip[] equips)
    {
        for (var i = 0; i < stops.Length; i++)
        {
            if (i < equips.Length)
            {
                stops[i].GetComponentInChildren<Text>().text = equips[i].GetEquipName();
            }
            else
            {
                stops[i].GetComponentInChildren<Text>().text = "Blocked";
            }
        }
    }

    IEnumerator Spin()
    {
        var stoppeds = new bool[] { false, false };
        var i = 0;

        while (stoppeds.Any(s => !s))
        {
            for (i = 0; i < spinners.Length; i++)
            {
                currentAngles[i] = Mathf.Lerp(currentAngles[i], targetAngles[i], Time.deltaTime);
                spinners[i].rotation = Quaternion.Euler(0, 0, currentAngles[i]);

                if (!stoppeds[i])
                    stoppeds[i] = Mathf.Abs(targetAngles[i] - currentAngles[i]) <= 1;
            }
            yield return null;
        }

        prizePopUp.ShowPrizePopUp(prize.GetFrontEquipPrefab(), prize.GetBackEquipPrefab(), prize.GetEquipName());
    }

    public void OnBtnClick()
    {
        //if (!spinning)
        //{
        //    spinning = true;
        button.interactable = false;
        StartCoroutine(Spin());
        //}
    }

    void BlockSelf()
    {
        warningText.transform.parent.gameObject.SetActive(true);
        warningText.text = warning;
        button.interactable = false;
    }
}