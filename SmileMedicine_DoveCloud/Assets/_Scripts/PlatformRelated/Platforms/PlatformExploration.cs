using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformExploration : PlatformGeneral
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    public override void SwitchPlatform(GameObject player, PlatformPortal pp)
    {
        player.transform.position = new Vector3(0.0F, 0.0F, 0.0F);
        Debug.Log("PlatformPortal player teleport: " + player.transform.position);
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        

        platformSystem.gm.RespawnPlayerCharacter();
        Debug.Log("player Actual position: " + p.transform.position);
    }
    */
}
