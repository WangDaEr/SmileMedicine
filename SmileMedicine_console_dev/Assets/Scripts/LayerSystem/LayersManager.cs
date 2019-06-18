using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the movement of layers and processes the requests related to layers;
/// </summary>
public class LayersManager : MonoBehaviour
{
    public float switchSpeed;
    public float Y_offset;
    public float Z_offset;

    // Start is called before the first frame update
    void Start()
    {
        Set_LM_Reference();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// notify related layers to change their position;
    /// </summary>
    /// <param name="cur">the index of layer player is at currently</param>
    /// <param name="des">the index of layer player desires to move to</param>
    public void LayersTransformation(int cur, int des)
    {
        //int dif = cur - des;
        //Vector3 distance = new Vector3(0.0F, Y_offset * dif, Z_offset * dif);

        Vector3 movement = transform.GetChild(cur).position - transform.GetChild(des).position;

        Debug.Log("move: " + movement);

        foreach (Transform layer in transform)
        {
            layer.gameObject.GetComponent<LayerInformation>().StartLayerTransformation(layer.position + movement);
        }
    }

    /// <summary>
    /// register the layer manager to all gameobjects requiring its reference
    /// </summary>
    private void Set_LM_Reference()
    {
        foreach (Transform layer in transform)
        {
            layer.gameObject.GetComponent<LayerInformation>().LayerManager = this;
        }
    }
}
