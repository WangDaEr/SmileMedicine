using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the movement of layers and processes the requests related to layers;
/// </summary>
public class LayersManager : MonoBehaviour
{
    public GameManager gm;

    public float switchSpeed = 1;
    public float Y_Offset;
    public float Z_Offset;

    public float CylinderRadius;
    public float CylinderIntervalDegree;

    public List<GameObject> layers = new List<GameObject>();
    public int LayersNumber;

    public int layerIndex_init = 0;

    public int layerTranformationOption = 0;

    public bool useFakePespective;
    public int lastUnchangedLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        SetLMReference();
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
    public void LayersTransformation(int cur, int des, Vector3 des_pos)
    {
        //int dif = cur - des;
        //Vector3 distance = new Vector3(0.0F, Y_offset * dif, Z_offset * dif);

        if (layerTranformationOption == 0)
        {
            //gm.playerCharacter.GetComponent<PlayerController>().StartSpecialMove(des_pos, switchSpeed, transform.GetChild(des).localScale);
            //gm.playerCharacter.GetComponent<PlayerController>().Z_restraint = false;
            gm.playerCharacter.GetComponent<PlayerController>().SetZAxisMovePara(des_pos, transform.GetChild(des).localScale);
            gm.playerCharacter.GetComponent<PlayerController>().StartZMove_OneStep();

            Debug.Log("%%%%%%%%%%% " + gm.playerCharacter.GetComponent<PlayerController>().Z_restraint);
        }
        else if(layerTranformationOption == 1)
        {
            Vector3 movement = transform.GetChild(cur).position - transform.GetChild(des).position;

            Debug.Log("!!!!!!!!!!!!!!!!!!move: " + movement);

            foreach (Transform layer in transform)
            {
                layer.gameObject.GetComponent<LayerInformation>().StartLayerTransformation(layer.position + movement, cur - des);
            }
        }

        
    }

    /// <summary>
    /// register the layer manager to all gameobjects requiring its reference
    /// </summary>
    private void SetLMReference()
    {
        foreach (Transform layer in transform)
        {
            layer.gameObject.GetComponent<LayerInformation>().LayerManager = this;
        }
    }
}
