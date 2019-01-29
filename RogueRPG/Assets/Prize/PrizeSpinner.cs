using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PrizeSpinner : MonoBehaviour
{
    public RectTransform target;
    Quaternion rotation;
    Vector3 currentVector3;
    Vector3 targetVector3;

    Equip prize;
    [SerializeField] RectTransform[] stops;
    Vector3[] currentDirections;
    Vector3[] targetDirections;
    [SerializeField] Transform[] spinners;
    Button button;
    bool spinning;
    [SerializeField] Text warningText;
    [SerializeField] string warning;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        warningText.transform.parent.gameObject.SetActive(false);
        Initialize(0, 2);
    }

    public void Initialize(int charIndex, int dungeonLevel)
    {
        //TODO pegar target direction
        //TODO vou ter que filtrar ainda pelo level dos equips e o level da missão
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
        currentDirections = new Vector3[spinners.Length];
        targetDirections = new Vector3[spinners.Length];

        for (var i = 0; i < spinners.Length; i++)
        {
            currentDirections[i] = spinners[i].rotation.eulerAngles;
            var direction = stops[stopsIndex[i]].position - spinners[i].position;
            var angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            targetDirections[i] = new Vector3(0, 0, angle * 55);
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
                currentDirections[i] = Vector3.Slerp(currentDirections[i], targetDirections[i], Time.deltaTime);
                spinners[i].rotation = Quaternion.Euler(currentDirections[i]);

                if (!stoppeds[i])
                    stoppeds[i] = Mathf.Abs((currentDirections[i] - targetDirections[i]).z) <= 1;
            }
            yield return null;
        }
        print("Parou de rodar, solta o som! O prêmio era " + prize.GetEquipName() + "!!");
    }

    public void OnBtnClick()
    {
        if (!spinning)
        {
            spinning = true;
            StartCoroutine(Spin());
        }
    }

    void BlockSelf()
    {
        warningText.transform.parent.gameObject.SetActive(true);
        warningText.text = warning;
        button.interactable = false;
    }
}