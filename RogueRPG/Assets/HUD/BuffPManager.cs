using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPManager : MonoBehaviour, IBuffHUD
{

    [SerializeField] List<Sprite> spritesBuff;
    [SerializeField] List<Sprite> spritesDebuff;
    [SerializeField] BuffParticles buffParticlesPrefab;
    List<BuffParticles> buffsParticles = new List<BuffParticles>();
    Transform canvasTransform;

    private void Awake()
    {
        canvasTransform = FindObjectOfType<Canvas>().transform;
        buffsParticles.Add(Instantiate(buffParticlesPrefab));
        buffsParticles[0].transform.SetParent(transform);
    }

    public void PlayAt(Character character, Attribute.Type stats, Attribute.Intensity intensity, Vector2 position)
    {
        Sprite sprite;
        if (((int)intensity) % 2 == 0)
            sprite = spritesBuff[(int)stats];
        else
            sprite = spritesDebuff[(int)stats];
        BuffParticles bp = buffsParticles.Find(b => b.Owner == character && b.Stats == stats);
        if (bp == null)
        {
            if (AvailableBuffParticles.Count > 0)
            {
                bp = AvailableBuffParticles[0];
            }
            else
            {

                buffsParticles.Add(Instantiate(buffParticlesPrefab));
                bp = buffsParticles[buffsParticles.Count - 1];
            }
            bp.transform.SetParent(canvasTransform);
        }
        bp.PlayAt(character, (int)intensity, position, sprite, stats);
    }

    public void Stop(Character character, Attribute.Type stats)
    {
        BuffParticles bp = buffsParticles.Find(b => b.Owner == character && b.Stats == stats);
        if (bp != null)
        {
            bp.transform.SetParent(transform);
            bp.Stop();
        }
    }

    List<BuffParticles> AvailableBuffParticles
    {
        get
        {
            return buffsParticles.FindAll(bp => bp.Owner == null);
        }
    }
}