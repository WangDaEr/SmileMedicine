using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformPortal))]
public class PlatformPortalInspector : Editor
{
    private PlatformPortal pp;

    private GameObject ConnectPortal
    {
        get { return pp.connectedPortal; }
        set
        {
            pp.connectedPortal = value;
            if (value) { value.GetComponent<PlatformPortal>().connectedPortal = pp.gameObject; }
        }
    }

    private void OnEnable()
    {
        pp = (PlatformPortal)target;
        if (!pp.platform)
        {
            Transform platform = pp.transform.parent;
            bool find = true;
            while (platform.tag != "Platform")
            {
                platform = platform.parent;
                if (!platform) { find = false; break; }
            }

            if (find)
            {
                pp.platform = platform.GetComponent<PlatformGeneral>();
                Debug.Log("Find Platform: " + pp.platform.index);
            }
            else { /*display error message;*/ }
        }
    }


    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        pp.portalActive = EditorGUILayout.Toggle(new GUIContent("Active Portal"), pp.portalActive);

        pp.portalType = (PlatformPortal.PlatformPortalType)EditorGUILayout.EnumPopup(new GUIContent("Portal Type"), pp.portalType);

        if (pp.portalType == PlatformPortal.PlatformPortalType.Exit)
        {
            ConnectPortal = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Destination Portal"), ConnectPortal, typeof(GameObject), true);
            if (ConnectPortal == null || pp.connectedPortal.GetComponent<PlatformPortal>() == null)
            {
                //display error message;
            }
        }
        else
        {
            GUIContent connectedPortalName = new GUIContent("Previous Portal not Set");
            GUIContent previousPlatformIndex = new GUIContent("");

            if (ConnectPortal)
            {
                connectedPortalName.text = "Platform of Previous Portal: ";
                PlatformGeneral pre = ConnectPortal.GetComponent<PlatformPortal>().platform;
                previousPlatformIndex.text = "Row: " + (int)pre.index.x + " Column: " + (int)pre.index.y;
            }

            EditorGUILayout.LabelField(connectedPortalName, previousPlatformIndex);
        }

        EditorGUILayout.EndVertical();
    }
}
