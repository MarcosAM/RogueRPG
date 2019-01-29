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

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        Initialize(0, 2);
    }

    //private void Start()
    //{
    //    Vector2 direction = target.transform.position - transform.position;
    //    float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
    //    currentVector3 = transform.rotation.eulerAngles;
    //    targetVector3 = new Vector3(0, 0, angle * 55);
    //}

    public void Initialize(int charIndex, int dungeonLevel)
    {
        //TODO pegar target direction
        //TODO vou ter que filtrar ainda pelo level dos equips e o level da missão
        //TODO alterar para ter equipamentos de nível 0 mesmo 
        var validEquips = PartyManager.GetParty()[charIndex].GetEquips().Where(e => e.GetLevel() <= dungeonLevel).ToArray();

        if (validEquips.Length <= 1)
        {
            //BlockMyself
        }

        UpdateSlotsTexts(validEquips);

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
            print("Teste");
            for (i = 0; i < spinners.Length; i++)
            {
                currentDirections[i] = Vector3.Slerp(currentDirections[i], targetDirections[i], Time.deltaTime);
                spinners[i].rotation = Quaternion.Euler(currentDirections[i]);

                if (!stoppeds[i])
                    stoppeds[i] = currentDirections[i] == targetDirections[i];
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

    //void Update()
    //{
    //    if (spinning)
    //    {
    //        transform.Rotate(Vector3.forward, 200 * Time.deltaTime);
    //    }
    //    else
    //    {
    //        currentVector3 = Vector3.Slerp(currentVector3, targetVector3, 1 * Time.deltaTime);
    //        transform.rotation = Quaternion.Euler(currentVector3);
    //    }
    //}
}

//currentDirection1 = spinner1.rotation.eulerAngles;
//        currentDirection2 = spinner2.rotation.eulerAngles;

//        var direction1 = stops[random1].position - spinner1.position;
//var direction2 = stops[random2].position - spinner2.position;
//var angle1 = Mathf.Atan2(-direction1.x, direction1.y) * Mathf.Rad2Deg;
//var angle2 = Mathf.Atan2(-direction2.x, direction2.y) * Mathf.Rad2Deg;
//targetDirection1 = new Vector3(0, 0, angle1* 55);
//targetDirection2 = new Vector3(0, 0, angle2* 55);