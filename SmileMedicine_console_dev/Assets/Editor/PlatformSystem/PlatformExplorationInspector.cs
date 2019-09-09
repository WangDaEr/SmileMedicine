using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor((typeof(PlatformExploration)))]
public class PlatformExplorationInspector : Editor
{
    private PlatformExploration pe;

    private Vector3 cameraCentre;
    public Vector3 CameraCentre { get { return cameraCentre; } set { cameraCentre = value; pe.cameraCentre = value; } }

    private PlatformSystem.PlatformType platformType;
    public PlatformSystem.PlatformType PlatformType { get { return platformType; } set { platformType = value; pe.platformType = value; } }

    private Vector2 platformSize;
    public Vector2 PlatformSize { get { return platformSize; } set { platformSize = value; pe.platformSize = value; } }

    private void OnEnable()
    {
        pe = (PlatformExploration)target;

        cameraCentre = pe.cameraCentre;
        platformType = pe.platformType;
    }

    private void bindCurCameraPos()
    {
        CameraCentre = Camera.main.transform.position;
    }

    private void defaultPlatformSize()
    {
        List<Vector2> size = new List<Vector2>(2);
        Transform layerManager = null;
        foreach (Transform child in pe.transform)
        {
            if (child.tag == "LayersManager")
            {
                layerManager = child;
                break;
            }
        }

        if (layerManager != null)
        {
            foreach (Transform child in layerManager)
            {
                if (child.GetComponent<Renderer>())
                {
                    Vector3 r = child.GetComponent<Renderer>().bounds.extents;
                    if (child.position.x + r.x > size[0].x)
                    {
                        Vector2 t = size[0];
                        t.x = child.position.x + r.x;
                        size[0] = t;
                    }
                    if (child.position.x - r.x < size[0].y)
                    {
                        Vector2 t = size[0];
                        t.y = child.position.x - r.x;
                        size[0] = t;
                    }
                    if (child.position.y + r.y > size[1].x)
                    {
                        Vector2 t = size[1];
                        t.x = child.position.y + r.y;
                        size[1] = t;
                    }
                    if (child.position.y + r.y > size[1].x)
                    {
                        Vector2 t = size[1];
                        t.x = child.position.y + r.y;
                        size[1] = t;
                    }
                }
            }

            Vector2 s = new Vector2(size[0].x - size[0].y, size[1].x - size[1].y);
            PlatformSize = s;
            pe.platformCenter = new Vector2((size[0].x - size[0].y) / 2, (size[1].x - size[1].y) / 2);
        }
        else
        {
            //display error message;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        PlatformType = (PlatformSystem.PlatformType)EditorGUILayout.EnumPopup(new GUIContent("Platform Type"), PlatformType);

        CameraCentre = EditorGUILayout.Vector3Field(new GUIContent("Camera Position for Current Platform"), CameraCentre);
        if (GUILayout.Button(new GUIContent("Use Current Camera Position")))
        {
            bindCurCameraPos();
        }

        PlatformSize = EditorGUILayout.Vector2Field(new GUIContent("Platform Size"), PlatformSize);
        if (GUILayout.Button(new GUIContent("Use Default Platform Size")))
        {
            defaultPlatformSize();
        }

        EditorGUILayout.EndVertical();
    }
}
