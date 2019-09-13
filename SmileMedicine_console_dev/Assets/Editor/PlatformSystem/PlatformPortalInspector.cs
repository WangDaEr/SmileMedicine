using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformPortal))]
public class PlatformPortalInspector : Editor
{
    private PlatformPortal pp;

    private void OnEnable()
    {
        pp = (PlatformPortal)target;
    }


    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        pp.portalType = (PlatformPortal.PlatformPortalType)EditorGUILayout.EnumPopup(new GUIContent("Portal Type"), pp.portalType);
        if (pp.portalType == PlatformPortal.PlatformPortalType.Exit)
        {
            pp.desPortal = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Destination Portal"), pp.desPortal, typeof(GameObject), true);
            if (pp.desPortal == null || pp.desPortal.GetComponent<PlatformPortal>() == null)
            {
                pp.desPortal = null;
                //display error message;
            }
        }

        EditorGUILayout.EndVertical();
    }
}
