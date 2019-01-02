using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillToggleManager : MonoBehaviour
{

    private TurnManager combatBehavior;
    [SerializeField] private List<SkillToggle> skillToggles = new List<SkillToggle>();
    private PlayerInputManager playerInputManager;
    private ToggleGroup toggleGroup;

    void Awake()
    {
        skillToggles = GetComponentsInChildren<SkillToggle>().OfType<SkillToggle>().ToList();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public bool AnyToggleOne() { return toggleGroup.AnyTogglesOn(); }

    public void OnAnyToggleChange(SkillToggle skillToggle)
    {
        if (skillToggle.getToggle().isOn)
        {
            playerInputManager.ReportNewSelectedSkillToggle(skillToggle);
            return;
        }
        playerInputManager.ReportNewSelectedSkillToggle(null);
    }

    public void SetAllSkillTogglesOff()
    {
        toggleGroup.SetAllTogglesOff();
    }

    public void ShowSkillTogglesFor(Equip equip)
    {
        gameObject.SetActive(true);

        HideSkillToggles();

        for (int i = 0; i < equip.GetSkills().Count; i++)
        {
            skillToggles[i].gameObject.SetActive(true);
            skillToggles[i].getText().text = equip.GetSkills()[i].GetSkillName();
        }
    }

    public int GetSelectedSkillIndex()
    {
        for (int i = 0; i < skillToggles.Count; i++)
        {
            if (skillToggles[i].getToggle().isOn)
            {
                return i;
            }
        }
        return 1;
    }

    public void HideSkillToggleMananger()
    {
        SetAllSkillTogglesOff();
        gameObject.SetActive(false);
    }

    void HideSkillToggles()
    {
        foreach (SkillToggle skillToggle in skillToggles)
        {
            skillToggle.gameObject.SetActive(false);
        }
    }
}
