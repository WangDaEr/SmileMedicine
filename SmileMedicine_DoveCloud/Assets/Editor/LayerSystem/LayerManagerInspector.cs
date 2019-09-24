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

    private bool useFakePespective;

    public bool UseFakePespective { get { return useFakePespective; } set { useFakePespective = value; lm.useFakePespective = value; } }

    void OnEnable()
    {
        lm = (LayersManager)target;

        layersNumber = lm.layers.Count;
        layerIndex_init = lm.layerIndex_init;
        layerTranformationOption = (LayerTransformationOptions)lm.layerTranformationOption;
        y_Offset = lm.Y_Offset;
        z_Offset = lm.Z_Offset;
        cylinderIntervalDegree = lm.CylinderIntervalDegree;
        cylinderRadius = lm.CylinderRadius;
        switchSpeed = lm.switchSpeed;
        useFakePespective = lm.useFakePespective;

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

    private void ChangeScaleForFakePesp()
    {
        if (useFakePespective)
        {
            Vector3 cameraPos =  GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            Vector3 unitLayerPos = lm.transform.GetChild(lm.layerIndex_init).position;
            Debug.Log("pos: " + cameraPos);

            float unit_dis = unitLayerPos.z - cameraPos.z;

            foreach (Transform layer in lm.transform)
            {
                //only affect layers in front of camera;
                if (layer.position.z <= cameraPos.z)
                {
                    lm.lastUnchangedLayerIndex = layer.GetComponent<LayerInformation>().layerIndex;
                    continue;
                }

                float distanceToUnitLayer = layer.position.z - unitLayerPos.z;
                float scale_ratio = unit_dis / (unit_dis + distanceToUnitLayer);

                Vector3 newScale = new Vector3(scale_ratio, scale_ratio, scale_ratio);
                if (layer.localScale != newScale)
                {
                    layer.localScale = newScale;
                }
            }
        }
        else
        {
            foreach (Transform layer in lm.transform)
            {
                layer.localScale = new Vector3(1.0F, 1.0F, 1.0F);
            }
        }
    }

    private void InstantiateLayers()
    {
        bool isOneDir = layerTranformationOption == LayerTransformationOptions.OneDirection;

        for (int i = 0; i < lm.layers.Count; ++i)
        {
            float index_diff = i - layerIndex_init;

            Vector3 newTranslation = new Vector3(
                0.0F,
                isOneDir ? index_diff * Y_Offset : CylinderRadius,
                isOneDir ? index_diff * Z_Offset : 0.0F
                );

            GameObject newObj = Instantiate(lm.layers[i], lm.transform);

            newObj.transform.position = new Vector3(0.0F, 0.0F, 0.0F);

            if (!isOneDir)
            {
                newObj.transform.Rotate(Vector3.right, index_diff * CylinderIntervalDegree, Space.World);
            }
            
            newObj.transform.Translate(newTranslation, Space.Self);


            if (!isOneDir)
            {
                newObj.transform.Translate(new Vector3(newTranslation.x, -newTranslation.y, 0.0F), Space.World);
            }
        }

        ChangeScaleForFakePesp();
    }

    private void UpdateLayersPosition()
    {
        bool isOneDir = layerTranformationOption == LayerTransformationOptions.OneDirection;
        for (int i = 0; i < lm.transform.childCount; ++i)
        {
            float index_diff = i - layerIndex_init;
            Transform layer = lm.transform.GetChild(i);

            Vector3 newTranslation = new Vector3(
                0.0F,
                isOneDir ? index_diff * Y_Offset : 0.0F,
                isOneDir ? index_diff * Z_Offset : CylinderRadius
                );


            layer.transform.rotation = Quaternion.Euler(new Vector3(0.0F, 0.0F, 0.0F));
            layer.transform.position = new Vector3(0.0F, 0.0F, 0.0F);

            if (!isOneDir)
            {
                layer.transform.Rotate(Vector3.right, index_diff * CylinderIntervalDegree, Space.World);
            }

            layer.transform.Translate(newTranslation, Space.Self);

            if (!isOneDir)
            {
                layer.transform.Translate(new Vector3(newTranslation.x, -newTranslation.z, 0.0F), Space.World);
            }
        }

        ChangeScaleForFakePesp();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        CheckLayerNumber(EditorGUILayout.IntSlider(new GUIContent("Number of Layers", "the number of layer ready to create"), layersNumber, 0, 10));

        LayerIndex_init = EditorGUILayout.IntSlider(new GUIContent("Index of Initial Layer", "The index of layer that will be used for player spawn place"), layerIndex_init, 0, layersNumber);

        CheckLayer_T_Option((LayerTransformationOptions)EditorGUILayout.EnumPopup(new GUIContent("Layer Transformation Option", "the layout of layers;\nlayers can either be on a plane or the side of cylinder"), layerTranformationOption));

        if (layerTranformationOption == LayerTransformationOptions.OneDirection)
        {
            Z_Offset = EditorGUILayout.Slider(new GUIContent("Depth", "how far the layer is away from camera on Z axis"), Z_Offset, 0, 15);
            Y_Offset = EditorGUILayout.Slider(new GUIContent("Height", "how high the layer is above the original plane on Y axis"), Y_Offset, 0, 15);

            UseFakePespective = EditorGUILayout.Toggle(new GUIContent("Use Fake Pespective"), UseFakePespective);
        }
        else if (layerTranformationOption == LayerTransformationOptions.Cyclical)
        {
            CylinderRadius = EditorGUILayout.Slider(new GUIContent("Cylinder Radius", "if all layers are on the side of a cylinder, the radius of it"), cylinderRadius, 0, 15);
            CylinderIntervalDegree = EditorGUILayout.Slider(new GUIContent("Interval Degree", "if all layers are on the side of a cylinder, degree between each layer"), cylinderIntervalDegree, 0, layersNumber > 0 ? 360 / layersNumber : 180);
        }

        SwitchSpeed = EditorGUILayout.Slider(new GUIContent("Transform Speed", "One Direction: translation on Z axis (units)\nCyclical: rotation around X axis (degree)"), switchSpeed, 0, layerTranformationOption == LayerTransformationOptions.OneDirection ? 10 : 30);

        for (int i = 0; i < lm.layers.Count; ++i)
        {
            //Debug.Log("length: " + lm.layers.Count + " " + i + " " + layersNumber);
            lm.layers[i] = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Layer " + i, "the layer at index " + i + " (0 is the one farest from camera)"), lm.layers[i], typeof(GameObject), false);
        }

        if (GUILayout.Button(new GUIContent("Instantiate All Layers", "create the layers in the scene")))
        {
            InstantiateLayers();
        }

        if (GUILayout.Button(new GUIContent("Update Positions of All Layers", "update states of layers in the scene")))
        {
            UpdateLayersPosition();
        }

        if (GUILayout.Button(new GUIContent("Destroy All Layers", "remove all the layers in the scene under current layer manager")))
        {
            DestroyLayers();
        }

        EditorGUILayout.EndVertical();
    }
}
