using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameManager gm;

    private void Awake()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameManager");
        if (go)
        {
            gm = go.GetComponent<GameManager>();
        }
        else
        {
            //display error message;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Player")
        {
            gm.cp = this;
        }
    }
}
