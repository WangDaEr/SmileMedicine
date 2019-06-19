using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LayersManager))]
public class LayerManagerInspector : Editor
{
    public enum LayerTransformationOptions
    {
        OneDirection,
        Cyclical
    }

    LayersManager lm;

    private int layersNumber = 0;
    public int LayersNumber { get { return layersNumber; } set { layersNumber = value; lm.LayersNumber = value; } }

    private int layerIndex_init = 0;
    public int LayerIndex_init { get { return layerIndex_init; } set { layerIndex_init = value; lm.layerIndex_init = value; } }

    private LayerTransformationOptions layerTranformationOption = LayerTransformationOptions.OneDirection;
    public LayerTransformationOptions LayerTransformationOption { get { return layerTranformationOption; } set { layerTranformationOption = value; lm.layerTranformationOption = (int)value; } }

    private float y_Offset = 0.0F;
    public float Y_Offset { get { return y_Offset; } set { y_Offset = value; lm.Y_Offset = value; } }

    private float z_Offset = 0.0F;
    public float Z_Offset { get { return z_Offset; } set { z_Offset = value; lm.Z_Offset = value; } }

    private float cylinderRadius = 0.0F;
    public float CylinderRadius { get { return cylinderRadius; } set { cylinderRadius = value; lm.CylinderRadius = value; } }

    private float cylinderIntervalDegree = 0.0F;
    public float CylinderIntervalDegree { get { return cylinderIntervalDegree; } set { cylinderIntervalDegree = value; lm.CylinderIntervalDegree = value; } }

    private float switchSpeed = 0.0F;
    public float SwitchSpeed { get { return switchSpeed; } set { switchSpeed = value; lm.switchSpeed = value; } }

    void OnEnable()
    {
        lm = (LayersManager)target;

        layersNumber = lm.transform.childCount;
        layerIndex_init = lm.layerIndex_init;
        layerTranformationOption = (LayerTransformationOptions)lm.layerTranformationOption;
        y_Offset = lm.Y_Offset;
        z_Offset = lm.Z_Offset;
        cylinderIntervalDegree = lm.CylinderIntervalDegree;
        cylinderRadius = lm.CylinderRadius;
        switchSpeed = lm.switchSpeed;

        Debug.Log("op: " + layerTranformationOption);
    }

    private void CheckLayerNumber(int newLayerNum)
    {
        if (layersNumber != newLayerNum)
        {
            layersNumber = newLayerNum;

            lm.layers.Clear();
            for (int i = 0; i < layersNumber; ++i)
            {
                //GameObject placeHolder = new GameObject();
                lm.layers.Add(null);
                //GameObject newLayer = new GameObject();
                //newLayer.transform.SetParent(lmc.transform);
                //lmc.layers[i] = (GameObject)EditorGUILayout.ObjectField("Layer " + i, lmc.layers[i], typeof(GameObject), false);
            }

        }


    }

    private void CheckLayer_T_Option(LayerTransformationOptions newOption)
    {
        if (LayerTransformationOption != newOption)
        {
            LayerTransformationOption = newOption;

            DestroyLayers();
        }
    }

    private void DestroyLayers()
    {
        while (lm.transform.childCount > 0)
        {
            DestroyImmediate(lm.transform.GetChild(0).gameObject);
        }
    }

    private void InstantiateLayers()
    {
        bool isOneDir = layerTranformationOption == LayerTransformationOptions.OneDirection;

        for (int i = 0; i < lm.layers.Count; ++i)
        {
            float index_diff = i - LayerIndex_init;

            Vector3 newTranslation = new Vector3(
                0.0F,
                isOneDir ? index_diff * -Y_Offset : 0.0F,
                isOneDir ? index_diff * Z_Offset : CylinderRadius
                );
            /*
            Vector3 newRot_v3 = new Vector3(
                isOneDir ? 0.0F : index_diff * CylinderIntervalDegree,
                0.0F,
                0.0F
                );
            */

            GameObject newObj = Instantiate(lm.layers[i], lm.transform);

            newObj.transform.position = new Vector3(0.0F, 0.0F, 0.0F);

            if (!isOneDir)
            {
                newObj.transform.Rotate(Vector3.right, index_diff * CylinderIntervalDegree, Space.World);
            }
            
            newObj.transform.Translate(newTranslation, Space.Self);


            if (!isOneDir)
            {
                newObj.transform.Translate(new Vector3(newTranslation.x, -newTranslation.z, 0.0F), Space.World);
            }
        }
    }

    private void UpdateLayersPosition()
    {
        bool isOneDir = layerTranformationOption == LayerTransformationOptions.OneDirection;
        for (int i = 0; i < lm.transform.childCount; ++i)
        {
            float index_diff = i - LayerIndex_init;
            Transform layer = lm.transform.GetChild(i);

            Vector3 newTranslation = new Vector3(
                0.0F,
                isOneDir ? index_diff * -Y_Offset : 0.0F,
                isOneDir ? index_diff * Z_Offset : CylinderRadius
                );


            layer.transform.rotation = Quaternion.Euler(new Vector3(-90.0F, 0.0F, 0.0F));
            layer.transform.position = new Vector3(0.0F, 0.0F, 0.0F);

            layer.transform.Rotate(Vector3.right, index_diff * CylinderIntervalDegree, Space.World);
            layer.transform.Translate(newTranslation, Space.Self);

            if (!isOneDir)
            {
                layer.transform.Translate(new Vector3(newTranslation.x, -newTranslation.z, 0.0F), Space.World);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        CheckLayerNumber(EditorGUILayout.IntSlider("Number of Layers", layersNumber, 0, 10));

        LayerIndex_init = EditorGUILayout.IntSlider("Index of Initial Layer", layerIndex_init, 0, layersNumber);

        CheckLayer_T_Option((LayerTransformationOptions)EditorGUILayout.EnumPopup("Layer Transformation Option", layerTranformationOption));

        if (layerTranformationOption == LayerTransformationOptions.OneDirection)
        {
            Y_Offset = EditorGUILayout.Slider("Depth", Y_Offset, 0, 15);
            Z_Offset = EditorGUILayout.Slider("Height", Z_Offset, 0, 15);
        }
        else if (layerTranformationOption == LayerTransformationOptions.Cyclical)
        {
            CylinderRadius = EditorGUILayout.Slider("Cylinder Radius", cylinderRadius, 0, 15);
            CylinderIntervalDegree = EditorGUILayout.Slider("Interval Degree", cylinderIntervalDegree, 0, layersNumber > 0 ? 360 / layersNumber : 180);
        }

        SwitchSpeed = EditorGUILayout.Slider("Transform Speed", switchSpeed, 0, layerTranformationOption == LayerTransformationOptions.OneDirection ? 10 : 30);

        for (int i = 0; i < lm.layers.Count; ++i)
        {
            Debug.Log("length: " + lm.layers.Count + " " + i + " " + layersNumber);
            lm.layers[i] = (GameObject)EditorGUILayout.ObjectField("Layer " + i, lm.layers[i], typeof(GameObject), false);
        }

        if (GUILayout.Button("Instantiate All Layers"))
        {
            InstantiateLayers();
        }

        if (GUILayout.Button("Update Positions of All Layers"))
        {
            UpdateLayersPosition();
        }

        if (GUILayout.Button("Destroy All Layers"))
        {
            DestroyLayers();
        }

        EditorGUILayout.EndVertical();
    }
}
