using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneral : MonoBehaviour
{
    public PlatformSystem platformSystem;

    public Vector2 index;

    public PlatformSystem.PlatformType platformType = PlatformSystem.PlatformType.Exploration;
    public Vector3 cameraCentre;
    public Vector2 platformSize;
    public Vector2 platformCenter;
    public Vector2 platformPadding;

    private void Awake()
    {
        platformSystem = GameObject.FindGameObjectWithTag("PlatformSystem").GetComponent<PlatformSystem>();
    }

    protected virtual void activatePlatform()
    {
    }

    protected virtual void deactivatePlatform()
    {
    }

    //basic movement between platforms: teleport
    public virtual void SwitchPlatform(GameObject player, PlatformPortal pp)
    {
        player.GetComponent<CharacterController>().Move(pp.transform.position - player.transform.position);
    }

    /// <summary>
    /// The return value is ordered in "TopLeft, TopRight, BottomRight, BottomLeft"
    /// </summary>
    /// <returns></returns>
    public virtual List<Vector3> GetPlatformBoundingBoxCorners()
    {
        float hor_dis = platformSize.x / 2 + platformPadding.x;
        float ver_dis = platformSize.y / 2 + platformPadding.y;

        List<Vector3> ans = new List<Vector3>()
        {
            new Vector3(platformCenter.x - hor_dis, platformCenter.y + ver_dis, 0.0F),
            new Vector3(platformCenter.x + hor_dis, platformCenter.y + ver_dis, 0.0F),
            new Vector3(platformCenter.x + hor_dis, platformCenter.y - ver_dis, 0.0F),
            new Vector3(platformCenter.x - hor_dis, platformCenter.y - ver_dis, 0.0F)
        };

        return ans;
    }
}
