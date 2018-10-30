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

    public void ShowCombatants(List<Battleground.Tile> tiles)
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.Deinitialize();
            tileUI.gameObject.SetActive(false);
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            tileUIs[i].gameObject.SetActive(true);
            tileUIs[i].Initialize(tiles[i]);
            if (tiles[i].getOccupant() != null)
                tiles[i].getOccupant().setHUD(tileUIs[i]);
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


    //TODO Apagar esse negócio aqui!
    public void RefreshInitiativeHUD()
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.RefreshInitiative();
        }
    }

    public void ShowTargetBtns(Character user, Equip choosedSkill)
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.ShowTargetBtn(user, choosedSkill);
        }
    }

    //TODO Tirar essa bool!
    public void HideTargetBtns(bool asPreview)
    {
        foreach (TileUI tileUI in tileUIs)
        {
            tileUI.HideTargetBtn();
        }
    }


    public void onTargetBtnPressed(Battleground.Tile targetTile)
    {
        FindObjectOfType<PlayerInputManager>().ReturnEquipAndTarget(targetTile);
    }

    public void OnTargetBtnHoverEnter(TargetBtn targetBtn)
    {
        FindObjectOfType<PlayerInputManager>().HoverTargetBtnEnter(targetBtn);
    }

    public void PreviewTargets(Character user, Equip selectedEquip, Battleground.Tile target)
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


    //TODO Tirar isso aqui!
    public void startTurnOf(Character character)
    {
        foreach (TileUI tileUI in tileUIs)
        {
            if (tileUI.getCharacter() == character)
            {
                tileUI.showTargetBtnWithColor(Color.grey);
            }
            else
            {
                tileUI.HideTargetBtn();
            }
        }
    }

    //TODO Se livrar disso aqui tbm!
    public void endTurnOf(Character character)
    {
        character.getHUD().getAnimator().SetBool("Destaque", false);
    }

    public static CombHUDManager getInstance() { return instance; }

    public List<TileUI> GetTileUIs() { return tileUIs; }
}