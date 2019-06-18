using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManagerCyclical : MonoBehaviour
{

    public List<GameObject> layers = new List<GameObject>();
    public int LayersNumber;

    public int layerIndex_init = 0;

    public int layerTranformationOption = 0;

    public float Y_Offset;
    public float Z_Offset;

    public float CylinderRadius;
    public float CylinderIntervalDegree;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
