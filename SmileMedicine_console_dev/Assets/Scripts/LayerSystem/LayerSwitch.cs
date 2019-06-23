using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.activate a different interpolation of input (vertical movement for switching current layer);
/// 2.notify the layer manager to change the position of related layers
/// </summary>
public class LayerSwitch : MonoBehaviour
{
    public int target_layer_index_up = -1;
    public int target_layer_index_down = -1;
    public GameObject layerManager;

    public GameObject UpArrowPortal;
    public GameObject DownArrowPortal;

    private LayersManager lm;
    private int cur_layer_index;
    public int CurLayerIndex { get { return cur_layer_index; } set { cur_layer_index = value; } }
    private bool allowTranformation;
    private GameObject playerCharacter;

    // Start is called before the first frame update
    void Start()
    {        
        //get which layer the portal is at currently;
        cur_layer_index = transform.parent.gameObject.GetComponent<LayerInformation>().layerIndex;
        allowTranformation = false;

        layerManager = GameObject.FindGameObjectWithTag("LayersManager");
        lm = layerManager.GetComponent<LayersManager>();
        //lm = transform.parent.parent.parent.gameObject.GetComponent<LayersManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowTranformation && playerCharacter)
        {
            checkTranformation();
        }
    }

    /// <summary>
    /// choose the destination layer for transformation;
    /// </summary>
    private void checkTranformation()
    {
        float move_dir = playerCharacter.GetComponent<PlayerController>().m_input.ver_axis_val;

        /*
        if (target_layer_index_up > -1 && move_dir > 0.0F)
        {
            lm.LayersTransformation(cur_layer_index, target_layer_index_up);

            Debug.Log("from layer " + cur_layer_index + " to " + target_layer_index_up);
        }
        else if (target_layer_index_down > -1 && move_dir < 0.0F)
        {
            lm.LayersTransformation(cur_layer_index, target_layer_index_down);

            Debug.Log("from layer " + cur_layer_index + " to " + target_layer_index_down);
        }
        */
        if (UpArrowPortal && move_dir > 0.0F)
        {
            lm.LayersTransformation(cur_layer_index, UpArrowPortal.GetComponent<LayerSwitch>().CurLayerIndex, UpArrowPortal.transform.position);

            Debug.Log("from layer " + cur_layer_index + " to " + UpArrowPortal.GetComponent<LayerSwitch>().CurLayerIndex);
        }
        else if (DownArrowPortal && move_dir < 0.0F)
        {
            lm.LayersTransformation(cur_layer_index, DownArrowPortal.GetComponent<LayerSwitch>().CurLayerIndex, DownArrowPortal.transform.position);

            Debug.Log("from layer " + cur_layer_index + " to " + DownArrowPortal.GetComponent<LayerSwitch>().CurLayerIndex);
        }
    }

    /// <summary>
    /// enter the collision of the portal;
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject sor = other.gameObject;

        if (sor.tag == "Player") //currently it only allow move to one direction;
        {
            playerCharacter = sor;
            allowTranformation = true;

            Debug.Log("entered portal");
            sor.GetComponent<PlayerController>().movDir = "to another layer";
        }
        else
        {
            Debug.Log("asdasdasdasd");
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
            playerCharacter = null;
            allowTranformation = false;
            Debug.Log("left portal");
        }
    }
}
