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
    [SerializeField] Text warningText;
    [SerializeField] string warning;
    PrizePopUp prizePopUp;
    string resultTip;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        warningText.transform.parent.gameObject.SetActive(false);
        prizePopUp = GetComponentInChildren<PrizePopUp>();
    }

    public void Initialize(int charIndex, int dungeonLevel)
    {
        //TODO alterar para ter equipamentos de nível 0 mesmo
        prizePopUp.gameObject.SetActive(false);

        var validEquips = PartyManager.GetParty()[charIndex].GetEquips().Where(e => e.GetLevel() <= dungeonLevel).ToArray();

        UpdateSlotsImages(validEquips);

        if (validEquips.Length <= 1)
        {
            BlockSelf();
            return;
        }

        var random1 = Random.Range(0, validEquips.Length - 1);
        var random2 = Random.Range(0, validEquips.Length - 1);

        prize = EquipDatabase.UnlockNewEquip(validEquips[random1].GetArchetype(), validEquips[random2].GetArchetype(), validEquips[random1].GetLevel(), validEquips[random2].GetLevel(), dungeonLevel);

        var lowestLevel = validEquips[random1].GetLevel() > validEquips[random2].GetLevel() ? validEquips[random2].GetLevel() : validEquips[random1].GetLevel();
        var highestLevel = validEquips[random1].GetLevel() + validEquips[random2].GetLevel() > dungeonLevel ? dungeonLevel : validEquips[random1].GetLevel() + validEquips[random2].GetLevel();
        var amount = EquipDatabase.GetAllEquips().Where(e => (e.GetLevel() >= lowestLevel && e.GetLevel() <= highestLevel) && (e.GetArchetype() == validEquips[random2].GetArchetype() || e.GetArchetype() == validEquips[random1].GetArchetype())).ToArray().Length;

        resultTip = "Of All " + amount + " " + validEquips[random1].GetArchetype() + " Or " + validEquips[random2].GetArchetype() + " Equips From Levels " + lowestLevel + " to " + highestLevel + " You Won: " + prize.GetEquipName();

        PrepareSpinners(new int[] { random1, random2 });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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

    void UpdateSlotsImages(Equip[] equips)
    {
        for (var i = 0; i < stops.Length; i++)
        {
            if (i < equips.Length)
            {
                //stops[i].GetComponentInChildren<Text>().text = equips[i].GetEquipName();
                if (equips[i].GetBackEquipPrefab())
                {
                    Transform backEquip = Instantiate(equips[i].GetBackEquipPrefab());
                    backEquip.SetParent(stops[i]);
                    backEquip.localPosition = new Vector2(0, 0);
                    backEquip.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                }

                if (equips[i].GetFrontEquipPrefab())
                {
                    Transform frontEquip = Instantiate(equips[i].GetFrontEquipPrefab());
                    frontEquip.SetParent(stops[i]);
                    frontEquip.localPosition = new Vector2(0, 0);
                    frontEquip.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                }
            }
            else
            {
                //TODO trocar por uma cadeado
                //stops[i].GetComponentInChildren<Text>().text = "Blocked";
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

        if (prizePopUp)
        {
            prizePopUp.ShowPrizePopUp(prize.GetFrontEquipPrefab(), prize.GetBackEquipPrefab(), prize.GetEquipName());
            warningText.text = resultTip;
        }
        else
            print("Nops!");
    }

    public void OnBtnClick()
    {
        button.interactable = false;
        StartCoroutine(Spin());
    }

    void BlockSelf()
    {
        warningText.transform.parent.gameObject.SetActive(true);
        warningText.text = warning;
        button.interactable = false;
    }
}