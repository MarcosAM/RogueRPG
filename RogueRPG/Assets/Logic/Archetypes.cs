using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archetypes : MonoBehaviour
{
    [SerializeField] protected RectTransform[] hats;

    [SerializeField] RuntimeAnimatorController[] animators;
    static Archetypes instance;

    private void Awake()
    {
        instance = this;
    }

    public enum Archetype { Warlock, Wizard, Priest, Ranger, Thief, Fighter, Knight, Berserker }

    public static Archetype GetArchetype(Equip[] equips)
    {
        Archetype[] archetypes = equips.Select(equip => equip.GetArchetype()).ToArray();
        Dictionary<Archetype, int> counts = archetypes.GroupBy(equip => equip).ToDictionary(g => g.Key, g => g.Count());

        Archetype archetype = Archetype.Warlock;
        int amount = 0;

        foreach (KeyValuePair<Archetype, int> count in counts)
        {
            if (count.Key == archetype)
            {
                amount = count.Value;
            }
            else
            {
                if (count.Value > amount)
                {
                    archetype = count.Key;
                    amount = count.Value;
                }
            }
        }

        return archetype;
    }

    public static RectTransform GetHat(Archetype archetype) { return Instantiate(instance.hats[(int)archetype]); }

    public static RuntimeAnimatorController GetAnimator(Archetype archetype)
    {
        if (archetype > Archetype.Priest)
            return instance.animators[1];

        return instance.animators[0];
    }

    public static Sprite GetArchetypeIcon(Archetype archetype)
    {
        return instance.hats[(int)archetype].GetComponentInChildren<Image>().sprite;
    }
}