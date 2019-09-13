using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPortal : MonoBehaviour
{
    public enum PlatformPortalType
    {
        Enter,
        Exit
    }

    public PlatformPortalType portalType;
    public GameObject desPortal;
    public PlatformGeneral platform;
    public bool portalActive;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void changePlatform()
    {
    }
}
