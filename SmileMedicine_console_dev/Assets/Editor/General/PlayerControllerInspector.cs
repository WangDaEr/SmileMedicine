using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerInspector : Editor
{
    private PlayerController pc;

    private float runningSpeed;
    public float RunningSpeed { get { return runningSpeed; } set { runningSpeed = value; pc.runningSpeed = value; } }

    private float crouchingSpeed;
    public float CrouchingSpeed { get { return crouchingSpeed; } set { crouchingSpeed = value; pc.crouchingSpeed = value; } }

    private float climbingSpeed;
    public float ClimbingSpeed { get { return climbingSpeed; } set { climbingSpeed = value; pc.climbingSpeed = value; } }

    private float z_movementUnit;
    public float Z_movementUnit { get { return z_movementUnit; } set { z_movementUnit = value; pc.z_movementUnit = value; } }

    private float z_movementSpeed;
    public float Z_movementSpeed { get { return z_movementSpeed; } set { z_movementSpeed = value; pc.z_movementSpeed = value; } }

    private bool z_movementClip;
    public bool Z_movementClip { get { return z_movementClip; } set { z_movementClip = value; pc.z_movementClip = value; } }

    private float gravityFactor;
    public float GravityFactor { get { return gravityFactor; } set { gravityFactor = value; pc.gravityFactor = value; } }

    private void OnEnable()
    {
        pc = (PlayerController)target;

        runningSpeed = pc.runningSpeed;
        crouchingSpeed = pc.crouchingSpeed;
        climbingSpeed = pc.climbingSpeed;
        z_movementUnit = pc.z_movementUnit;
        z_movementSpeed = pc.z_movementSpeed;
        z_movementClip = pc.z_movementClip;
        gravityFactor = pc.gravityFactor;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        RunningSpeed = EditorGUILayout.FloatField(new GUIContent("Running Speed"), RunningSpeed);
        CrouchingSpeed = EditorGUILayout.FloatField(new GUIContent("Crouching Speed"), CrouchingSpeed);
        ClimbingSpeed = EditorGUILayout.FloatField(new GUIContent("Climbing Speed"), ClimbingSpeed);
        Z_movementUnit = EditorGUILayout.Slider(new GUIContent("Z Axis Movement Unit"), Z_movementUnit, 0, 10);
        Z_movementSpeed = EditorGUILayout.FloatField(new GUIContent("Z Axis Move Speed"), Z_movementSpeed);
        Z_movementClip = EditorGUILayout.Toggle(new GUIContent("Clip the Z Movement Unit"), Z_movementClip);

        GravityFactor = EditorGUILayout.FloatField(new GUIContent("Gravity For Player"), GravityFactor);

        EditorGUILayout.EndVertical();
    }
}
