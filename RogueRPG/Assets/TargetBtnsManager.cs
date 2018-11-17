using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtnsManager : MonoBehaviour
{
    [SerializeField] List<TargetBtn> targetBtns = new List<TargetBtn>();

    static TargetBtnsManager instance = null;

    public void Awake()
    {
        MakeItASingleton();
    }

    void MakeItASingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowTargetBtns(Character user, Equip choosedSkill)
    {
        foreach (TargetBtn targetBtn in targetBtns)
        {
            targetBtn.Appear(user, choosedSkill);
        }
    }

    public void HideTargetBtns()
    {
        foreach (TargetBtn targetBtn in targetBtns)
        {
            targetBtn.Disappear();
        }
    }


    public void OnTargetBtnPressed(Tile targetTile)
    {
        FindObjectOfType<PlayerInputManager>().ReturnEquipAndTarget(targetTile);
    }

    public void OnTargetBtnHoverEnter(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnEnter(targetBtn);
    }

    public void PreviewTargets(Character user, Equip selectedEquip, Tile target)
    {
        foreach (TargetBtn targetBtn in targetBtns)
        {
            targetBtn.CheckIfAffected(target, selectedEquip, user);
        }
    }

    public void OnTargetBtnHoverExit(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnExit(targetBtn);
    }

    public static TargetBtnsManager GetInstance() { return instance; }
}