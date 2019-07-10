using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerInspector : Editor
{
    private PlayerController_Independent pci;

    private float runningSpeed;
    public float RunningSpeed { get { return runningSpeed; } set { runningSpeed = value; pci.runningSpeed = value; } }

    private float crouchingSpeed;
    public float CrouchingSpeed { get { return crouchingSpeed; } set { crouchingSpeed = value; pci.crouchingSpeed = value; } }

    private float climbingSpeed;
    public float ClimbingSpeed { get { return climbingSpeed; } set { climbingSpeed = value; pci.climbingSpeed = value; } }

    private float z_movementUnit;
    public float Z_movementUnit { get { return z_movementUnit; } set { z_movementUnit = value; pci.z_movementUnit = value; } }

    private float gravityFactor;
    public float GravityFactor { get { return gravityFactor; } set { gravityFactor = value; pci.gravityFactor = value; } }

    private void OnEnable()
    {
        pci = (PlayerController)target;

        runningSpeed = pci.runningSpeed;
        crouchingSpeed = pci.crouchingSpeed;
        climbingSpeed = pci.climbingSpeed;
        z_movementUnit = pci.z_movementUnit;
        gravityFactor = pci.gravityFactor;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        RunningSpeed = EditorGUILayout.FloatField(new GUIContent("Running Speed"), RunningSpeed);
        CrouchingSpeed = EditorGUILayout.FloatField(new GUIContent("Crouching Speed"), CrouchingSpeed);
        ClimbingSpeed = EditorGUILayout.FloatField(new GUIContent("Climbing Speed"), ClimbingSpeed);
        Z_movementUnit = EditorGUILayout.Slider(new GUIContent("Z Axis Movement Unit"), Z_movementUnit, 0, 10);
        GravityFactor = EditorGUILayout.FloatField(new GUIContent("Gravity For Player"), GravityFactor);

        EditorGUILayout.EndVertical();
    }
}
