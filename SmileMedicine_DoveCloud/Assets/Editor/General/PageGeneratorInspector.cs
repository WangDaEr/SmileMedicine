using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PageGenerator))]
public class PageGeneratorInspector : Editor
{
    private PageGenerator pg;

    private void OnEnable()
    {
        pg = (PageGenerator)target;

        if (!pg.ps)
        {
            pg.ps = GameObject.FindGameObjectWithTag("PlatformSystem").GetComponent<PlatformSystem>();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("Generate Page Layout"))
        {
            if (pg.ps)
            {
                //pg.BuildPage();
                pg.BuildPagePrototype();
            }
            else
            {
                EditorGUILayout.LabelField(new GUIContent("The PlatforSystem for the PageGenerator is not Set" + pg.ps));
            }
        }

        EditorGUILayout.EndVertical();
    }
}
