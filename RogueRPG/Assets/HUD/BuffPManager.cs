using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPManager : MonoBehaviour, IBuffHUD
{

    [SerializeField] List<Sprite> spritesBuff;
    [SerializeField] List<Sprite> spritesDebuff;
    [SerializeField] BuffParticles buffParticlesPrefab;
    List<BuffParticles> buffParticles = new List<BuffParticles>();
    Canvas canvas;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        buffParticles.Add(Instantiate(buffParticlesPrefab));
        buffParticles[0].transform.SetParent(transform);
    }

    public void PlayAt(Character character, Stat.Stats stats, Stat.Intensity intensity, Vector2 position)
    {
        Sprite sprite;
        if ((int)stats % 2 == 0)
            sprite = spritesBuff[(int)stats];
        else
            sprite = spritesDebuff[(int)stats];
        if (AvailableBuffParticles.Count > 0)
        {
            AvailableBuffParticles[0].transform.SetParent(canvas.transform);
            AvailableBuffParticles[0].PlayAt(character, (int)intensity, position, sprite, stats);
        }
        else
        {
            Debug.Log(AvailableBuffParticles.Count);
            buffParticles.Add(Instantiate(buffParticlesPrefab));
            buffParticles[buffParticles.Count - 1].transform.SetParent(canvas.transform);
            buffParticles[buffParticles.Count - 1].PlayAt(character, (int)intensity, position, sprite, stats);
        }
    }

    public void Stop(Character character, Stat.Stats stats)
    {
        if (buffParticles.Find(bp => bp.Owner == character && bp.Stats == stats) != null)
        {
            buffParticles.Find(bp => bp.Owner == character && bp.Stats == stats).transform.SetParent(transform);
            buffParticles.Find(bp => bp.Owner == character && bp.Stats == stats).Stop();
        }
    }

    List<BuffParticles> AvailableBuffParticles
    {
        get
        {
            return buffParticles.FindAll(bp => bp.Owner == null);
        }
    }
}