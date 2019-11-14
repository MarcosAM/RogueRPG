using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonBtn : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] Sprite done;
    Text dungeonName;
    Button button;
    Image image;
    Dungeon dungeon;
    ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();

        image = GetComponentInChildren<Image>();

        var texts = GetComponentsInChildren<Text>();
        dungeonName = texts[0];
        button = GetComponentInChildren<Button>();

        Initialize(GameManager.getInstance().GetDungeon(index));
    }

    public void Initialize(Dungeon dungeon)
    {

        this.dungeon = dungeon;

        GameManager gameManager = GameManager.getInstance();

        if (gameManager.WasCleared(this.dungeon))
        {
            dungeonName.gameObject.SetActive(false);
            button.interactable = false;
            if (image && done)
            {
                image.sprite = done;
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            particleSystem.gameObject.SetActive(false);
            return;
        }

        int dungeonsCleared = gameManager.GetDungeonsCleared();

        if (dungeonsCleared < this.dungeon.GetPrerequisit())
        {
            dungeonName.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            particleSystem.Play();
        }

        if (dungeonsCleared == this.dungeon.GetPrerequisit())
        {
            ParticleSystem.MainModule psMain = particleSystem.main;
            psMain.startLifetime = new ParticleSystem.MinMaxCurve(3f, 6f);
            particleSystem.Play();
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