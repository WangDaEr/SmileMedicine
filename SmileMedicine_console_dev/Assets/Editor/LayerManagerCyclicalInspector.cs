using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LayerManagerCyclical))]
public class LayerManagerCyclicalInspector : Editor
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    LayerManagerCyclical lmc;

    private int layersNumber = 0;

    void OnEnable()
    {
        lmc = (LayerManagerCyclical)target;
        layersNumber = lmc.layers.Count;
    }

    private void CheckLayerNumber(int newLayerNum)
    {
        if (layersNumber != newLayerNum)
        {
            layersNumber = newLayerNum;

            foreach (GameObject go in lmc.layers)
            {
                DestroyImmediate(go);
            }

            lmc.layers = new List<GameObject>();
            for (int i = 0; i < layersNumber; ++i)
            {
                //GameObject placeHolder = new GameObject();
                //placeHolder.transform.SetParent(lmc.transform, true);
                lmc.layers.Add(null);
                //lmc.layers[i] = (GameObject)EditorGUILayout.ObjectField("Layer " + i, lmc.layers[i], typeof(GameObject), false);
            }
        }

        
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.BeginVertical();

        CheckLayerNumber(EditorGUILayout.IntSlider("Number of Layers", layersNumber, 0, 10));

        if (layersNumber < 5)
        {
            EditorGUILayout.HelpBox("12345", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox("678910", MessageType.Warning);
        }

        
        for (int i = 0; i < lmc.layers.Count; ++i)
        {
            Debug.Log("length: " + lmc.layers.Count + " " + i + " " + layersNumber);
            lmc.layers[i] = (GameObject)EditorGUILayout.ObjectField("Layer " + i, lmc.layers[i], typeof(GameObject), false);
        }
        
        EditorGUILayout.EndVertical();
    }
}
