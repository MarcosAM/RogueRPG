using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonBtn : MonoBehaviour
{
    [SerializeField] int index;
    Text dungeonName;
    Text dungeonLevel;
    Button button;
    Dungeon dungeon;

    void Start()
    {
        gameObject.SetActive(false);
        Initialize(GameManager.getInstance().GetDungeon(index));
    }

    public void Initialize(Dungeon dungeon)
    {

        this.dungeon = dungeon;

        switch (this.dungeon.GetState())
        {
            case Dungeon.State.Open:
            case Dungeon.State.Beaten:
                break;
            case Dungeon.State.JustDiscovered:
                //TODO iniciar a animaçãozinha feliz de ter sido acabado de ser descoberta
                this.dungeon.SetState(Dungeon.State.Open);
                break;
            default:
                return;
        }

        gameObject.SetActive(true);

        var texts = GetComponentsInChildren<Text>();
        dungeonName = texts[0];
        dungeonLevel = texts[1];
        button = GetComponentInChildren<Button>();

        dungeonName.text = dungeon.GetDungeonName();
        dungeonLevel.text = "Level " + dungeon.GetLevel();
    }

    public void OnBtnClick()
    {
        GameManager.getInstance().GoToDungeon(dungeon);
    }
}