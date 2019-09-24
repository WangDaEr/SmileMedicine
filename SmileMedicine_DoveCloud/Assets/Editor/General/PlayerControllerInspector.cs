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

    private bool z_test;
    public bool Z_test { get { return z_test; } set { z_test = value; pc.z_test = value;  if (value && !pc.SpecifyDestination) { pc.Z_restraint = !value; } /*pc.Z_restraint = !value; Debug.Log("Zrestraint:" + pc.Z_restraint);*/ } }

    private float gravityFactor;
    public float GravityFactor { get { return gravityFactor; } set { gravityFactor = value; pc.gravityFactor = value;} }

    private void OnEnable()
    {
        pc = (PlayerController)target;

        runningSpeed = pc.runningSpeed;
        crouchingSpeed = pc.crouchingSpeed;
        climbingSpeed = pc.climbingSpeed;
        z_movementUnit = pc.z_movementUnit;
        z_movementSpeed = pc.z_movementSpeed;
        z_movementClip = pc.z_movementClip;
        z_test = pc.z_test;
        gravityFactor = pc.gravityFactor;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        RunningSpeed = EditorGUILayout.FloatField(new GUIContent("Running Speed"), RunningSpeed);
        CrouchingSpeed = EditorGUILayout.FloatField(new GUIContent("Crouching Speed"), CrouchingSpeed);
        ClimbingSpeed = EditorGUILayout.FloatField(new GUIContent("Climbing Speed"), ClimbingSpeed);
        
        //Z_movementClip = EditorGUILayout.Toggle(new GUIContent("Clip the Z Movement Unit"), Z_movementClip);

        Z_test = EditorGUILayout.Toggle(new GUIContent("Test Z Axis Movement"), Z_test);
        if (Z_test)
        {
            Z_movementUnit = EditorGUILayout.Slider(new GUIContent("Z Axis Movement Unit"), Z_movementUnit, 0, 10);
        }
        Z_movementSpeed = EditorGUILayout.FloatField(new GUIContent("Z Axis Move Speed"), Z_movementSpeed);

        GravityFactor = EditorGUILayout.FloatField(new GUIContent("Gravity For Player"), GravityFactor);
        pc.fallLimit = EditorGUILayout.FloatField(new GUIContent("Lowest Y Value Player Can Reach"), pc.fallLimit);


        if (GUILayout.Button(new GUIContent("Use Player's Current Y Value")))
        {
            pc.fallLimit = pc.transform.position.y;
        }

        EditorGUILayout.EndVertical();
    }
}
