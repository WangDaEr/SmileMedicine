using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSystem : MonoBehaviour
{
    public enum PlatformType
    {
        Exploration,
        Interactive,
        Animation
    }

    public GameManager gm;

    public List<List<PlatformGeneral>> platforms = new List<List<PlatformGeneral>>();
    public GameObject backgroundPage;
    public Vector2 platformTotalSize;
    public Vector3 platformCenter;
    public Vector2 padding;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// The return value is ordered in "TopLeft, TopRight, BottomRight, BottomLeft"
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetPlatformSystemBoundingBoxCorners()
    {
        float hor_dis = platformTotalSize.x / 2 + padding.x;
        float ver_dis = platformTotalSize.y / 2 + padding.y;

        Debug.Log("PlatformSystemSize hor_dis: " + hor_dis + " " + ver_dis);

        List<Vector3> ans = new List<Vector3>()
        {
            new Vector3(platformCenter.x - hor_dis, platformCenter.y + ver_dis, 0.0F),
            new Vector3(platformCenter.x + hor_dis, platformCenter.y + ver_dis, 0.0F),
            new Vector3(platformCenter.x + hor_dis, platformCenter.y - ver_dis, 0.0F),
            new Vector3(platformCenter.x - hor_dis, platformCenter.y - ver_dis, 0.0F),
        };

        return ans;
    }
}
