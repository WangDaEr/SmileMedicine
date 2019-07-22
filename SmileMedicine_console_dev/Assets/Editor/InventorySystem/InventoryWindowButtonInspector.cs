using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryWindowButton))]
public class InventoryWindowButtonInspector : Editor
{
    // Start is called before the first frame update
    private InventoryWindowButton iwb;

    private GameObject bindedPanel;
    public GameObject BindedPanel { get { return bindedPanel; } set { bindedPanel = value; iwb.bindedPanel = value; } }

    private float panelInitialScale;
    public float PanelInitialScale
    {
        get
        {
            float scale = 0.0F;
            if (bindedPanel)
            {
                scale = bindedPanel.GetComponent<RectTransform>().localScale.x;
            }
            return scale;
        }
        set
        {
            panelInitialScale = value;
            iwb.panelInitialScale = value;

            if (bindedPanel)
            {
                bindedPanel.GetComponent<RectTransform>().localScale = new Vector3(value, value, value);
            }
        }
    }

    private float panelShowupTime;
    public float PanelShowupTime { get { return panelShowupTime; } set { panelShowupTime = value; iwb.panelShowupTime = value; } }

    private void OnEnable()
    {
        iwb = (InventoryWindowButton)target;

        bindedPanel = iwb.bindedPanel;
        panelInitialScale = iwb.panelInitialScale;
        panelShowupTime = iwb.panelShowupTime;
    }

    private void applyPanelCenterAligned()
    {
        if (bindedPanel)
        {
            bindedPanel.transform.position = iwb.transform.position;
        }
    }

    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.BeginVertical();

        BindedPanel = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Item Panel"), bindedPanel, typeof(GameObject), true);

        if (GUILayout.Button(new GUIContent("Align panel with window")))
        {
            applyPanelCenterAligned();
        }

        PanelInitialScale = EditorGUILayout.Slider(new GUIContent("Panel Initial Scale"), PanelInitialScale, 0.0F, 1.0F);

        PanelShowupTime = EditorGUILayout.FloatField(new GUIContent("Panel Showup Time"), panelShowupTime);

        EditorGUILayout.EndVertical();
    }
}
