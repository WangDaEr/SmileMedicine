using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneral : MonoBehaviour
{
    public PlatformSystem platformSystem;

    public PlatformSystem.PlatformType platformType = PlatformSystem.PlatformType.Exploration;
    public Vector3 cameraCentre;
    public Vector2 platformSize;
    public Vector2 platformCenter;
    public Vector2 platformMargin;

    protected virtual void activatePlatform()
    {
    }

    protected virtual void deactivatePlatform()
    {
    }
}
