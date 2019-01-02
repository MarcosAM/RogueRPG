using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffHUD
{
    void PlayAt(Character character, Attribute.Stats stats, Attribute.Intensity intensity, Vector2 position);
    void Stop(Character character, Attribute.Stats stats);
}
