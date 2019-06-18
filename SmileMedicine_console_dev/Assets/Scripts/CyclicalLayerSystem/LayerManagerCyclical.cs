using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManagerCyclical : MonoBehaviour
{

    public List<GameObject> layers = new List<GameObject>();
    public int LayersNumber;

    private void InstantiateLayers()
    {
        foreach (GameObject layer in layers)
        {
            GameObject newObj = Instantiate(layer, transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateLayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
