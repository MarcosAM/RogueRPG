using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombHUDManager : MonoBehaviour
{
    [SerializeField] List<TileUI> tileUIs = new List<TileUI>();

    static CombHUDManager instance = null;

    public void Awake()
    {
        MakeItASingleton();
    }

    //public void ShowCombatants(List<Tile> tiles)
    //{
    //    foreach (TileUI tileUI in tileUIs)
    //    {
    //        tileUI.Deinitialize();
    //        tileUI.gameObject.SetActive(false);
    //    }

    //    for (int i = 0; i < tiles.Count; i++)
    //    {
    //        tileUIs[i].gameObject.SetActive(true);
    //        tileUIs[i].Initialize(tiles[i]);
    //        if (tiles[i].getOccupant() != null)
    //            tiles[i].getOccupant().setHUD(tileUIs[i]);
    //    }
    //}

    public void ShowCombatants(List<Tile> tiles)
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.Deinitialize();
            tileUI.gameObject.SetActive(false);
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            //if (tiles[i].IsEnabled())
            //{
                tileUIs[i].gameObject.SetActive(true);
                tileUIs[i].Initialize(tiles[i]);
                if (tiles[i].getOccupant() != null)
                    tiles[i].getOccupant().setHUD(tileUIs[i]);
            //}
        }
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
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.ShowTargetBtn(user, choosedSkill);
        }
    }

    public void HideTargetBtns()
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.HideTargetBtn();
        }
    }


    public void onTargetBtnPressed(Tile targetTile)
    {
        FindObjectOfType<PlayerInputManager>().ReturnEquipAndTarget(targetTile);
    }

    public void OnTargetBtnHoverEnter(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnEnter(targetBtn);
    }

    public void PreviewTargets(Character user, Equip selectedEquip, Tile target)
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.CheckIfAffected(target, selectedEquip, user);
        }
    }

    public void OnTargetBtnHoverExit(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnExit(targetBtn);
    }

    public static CombHUDManager getInstance() { return instance; }

    public List<TileUI> GetTileUIs() { return tileUIs; }
}