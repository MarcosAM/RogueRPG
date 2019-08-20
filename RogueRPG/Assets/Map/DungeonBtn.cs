using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonBtn : MonoBehaviour
{
    [SerializeField] int index;
    Text dungeonName;
    Button button;
    Dungeon dungeon;
    ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();

        var texts = GetComponentsInChildren<Text>();
        dungeonName = texts[0];
        button = GetComponentInChildren<Button>();

        Initialize(GameManager.getInstance().GetDungeon(index));
    }

    public void Initialize(Dungeon dungeon)
    {

        this.dungeon = dungeon;

        int dungeonsCleared = GameManager.getInstance().GetDungeonsCleared();

        if (dungeonsCleared < this.dungeon.GetPrerequisit())
        {
            dungeonName.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            particleSystem.Play();
        }

        if (dungeonsCleared == this.dungeon.GetPrerequisit())
        {
            particleSystem.Stop();
        }

        if (dungeonsCleared > this.dungeon.GetPrerequisit())
        {
            particleSystem.gameObject.SetActive(false);
        }
        dungeonName.text = dungeon.GetDungeonName();
    }

    public void OnBtnClick()
    {
        GameManager.getInstance().GoToDungeon(dungeon);
    }
}