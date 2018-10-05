using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffHUD
{
    void PlayAt(Stat.Stats stats, Vector2 position);
    void Stop();
}
