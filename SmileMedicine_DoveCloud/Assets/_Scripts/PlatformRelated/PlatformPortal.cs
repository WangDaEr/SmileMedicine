using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformPortal : MonoBehaviour
{
    public enum PlatformPortalType
    {
        Enter,
        Exit
    }

    public PlatformPortalType portalType;
    public GameObject connectedPortal;
    public PlatformGeneral platform;
    public bool portalActive;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (portalType == PlatformPortalType.Enter) { return; }

        GameObject player = other.gameObject;

        if (player.tag == "Player" && portalActive && connectedPortal)
        {
            platform.SwitchPlatform(player, connectedPortal.GetComponent<PlatformPortal>());
            //Debug.Log("Player move between platforms from: " + platform.index + " to " + connectedPortal.GetComponent<PlatformPortal>().platform.index);
        }
    }

    private void changePlatform()
    {
    }
}
