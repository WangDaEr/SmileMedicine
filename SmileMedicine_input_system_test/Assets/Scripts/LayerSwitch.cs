using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.activate a different interpolation of input (vertical movement for switching current layer);
/// 2.notify the layer manager to change the position of related layers
/// </summary>
public class LayerSwitch : MonoBehaviour
{
    public int target_layer_index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// enter the collision of the portal;
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject sor = other.gameObject;

        if (sor.tag == "Player")
        {
            Debug.Log("entered portal");
        }
    }

    /// <summary>
    /// leave the collision of the portal;
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        GameObject sor = other.gameObject;

        if (sor.tag == "Player")
        {
            Debug.Log("left portal");
        }
    }

    /*
    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        GameObject sor = collision.gameObject;

        if (sor.tag == "Player")
        {

        }
    }
    */
}
