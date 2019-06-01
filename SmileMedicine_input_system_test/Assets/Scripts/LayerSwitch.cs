using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject sor = other.gameObject;

        if (sor.tag == "Player")
        {
            Debug.Log("entered portal");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject sor = other.gameObject;

        if (sor.tag == "Player")
        {
            Debug.Log("left portal");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject sor = collision.gameObject;

        if (sor.tag == "Player")
        {

        }
    }
}
