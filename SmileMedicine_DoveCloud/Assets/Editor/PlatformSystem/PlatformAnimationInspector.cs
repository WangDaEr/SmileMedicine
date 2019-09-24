using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformAnimation))]
public class PlatformAnimationInspector : Editor
{
    private PlatformAnimation pa;

    private void OnEnable()
    {
        pa = (PlatformAnimation)target;
        pa.platformType = PlatformSystem.PlatformType.Animation;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        pa.index = EditorGUILayout.Vector2Field(new GUIContent("Index for Current Platform"), pa.index);

        pa.platformSize = EditorGUILayout.Vector2Field(new GUIContent("Platform Size"), pa.platformSize);
        //pa.platformCenter = EditorGUILayout.Vector2Field(new GUIContent("Platform Center"), pa.platformCenter);

        if (GUILayout.Button("Use default Platform Position"))
        {
            Rect rec = pa.GetComponent<RectTransform>().rect;
            pa.platformSize = new Vector2(rec.width, rec.height);

            pa.platformCenter = pa.GetComponent<RectTransform>().transform.position;
        }
        pa.platformPadding = EditorGUILayout.Vector2Field(new GUIContent("Platform Padding"), pa.platformPadding);

        EditorGUILayout.EndVertical();
    }
}
