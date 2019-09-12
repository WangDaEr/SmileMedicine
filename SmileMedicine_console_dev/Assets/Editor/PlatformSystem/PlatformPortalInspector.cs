using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformPortal))]
public class PlatformPortalInspector : Editor
{
    private PlatformPortal pp;

    private int newPortalNum;

    private void OnEnable()
    {
        pp = (PlatformPortal)target;
        newPortalNum = pp.desPlatforms.Count;
    }

    private void checkPortalNum(int newPortalNum)
    {
        int dif = pp.desPlatforms.Count - newPortalNum;
        if (dif != 0)
        {
            if (dif < 0)
            {
                PlatformPortal[] newEle = new PlatformPortal[-dif];
                pp.desPlatforms.AddRange(newEle);
            }
            else
            {
                pp.desPlatforms.Clear();
            }
        }
        
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        pp.portalType = (PlatformPortal.PlatformPortalType)EditorGUILayout.EnumPopup(new GUIContent("Portal Type"), pp.portalType);
        if (pp.portalType == PlatformPortal.PlatformPortalType.Exit)
        {
            newPortalNum = EditorGUILayout.IntSlider(new GUIContent("Number of Destination"), newPortalNum, 0, 5);
            checkPortalNum(newPortalNum);

            for (int i = 0; i <pp.desPlatforms.Count; ++i)
            {
                GameObject go = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Destination Platform Portal" +  i), pp.desPlatforms[i], typeof(GameObject), true);
                if (go)
                {
                    pp.desPlatforms[i] = go.GetComponent<PlatformPortal>();
                }
                else
                {
                    //display error message;
                }
            }
        }

        EditorGUILayout.EndVertical();
    }
}
