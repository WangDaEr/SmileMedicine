﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// control the activity related to player character;
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public InputSystem m_input;
    public bool X_restraint;
    public bool Y_restraint;

    public float runningSpeed;
    public float crouchingSpeed;
    public float climbingSpeed;
    
    private CharacterController controller;
    private float curSpeed;
    private bool usingLadder;
    private string mov_dir;

    public string movDir
    {
        get
        {
            return mov_dir;
        }
        set
        {
            mov_dir = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        X_restraint = false;
        Y_restraint = true;  //initially the player can only move horizontally;

        mov_dir = string.Empty;
        usingLadder = false;
        curSpeed = 0.0F;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Interaction();
    }

    private void movement_debug_info(string pos_val, string neg_val, float check, float threshold, bool tag)
    {
        if (tag)
        {

        }
    }

    /// <summary>
    /// basic player movement in 2D space, horizontally and climbing;
    /// </summary>
    private void PlayerMove()
    {
        Vector3 movementTotal = new Vector3(0.0F, 0.0F, 0.0F);

        //add gravity?

        curSpeed = m_input.B_hold ? crouchingSpeed : runningSpeed;
        if (m_input.B_hold) { Debug.Log("crouching!"); }  //debug info (crouch)

        //check horizontal movement;
        if (!X_restraint && m_input.hor_axis_val != 0.0F)
        {
            Vector3 movementHor = transform.TransformDirection(Vector3.right);
            movementHor *= m_input.hor_axis_val * curSpeed * Time.deltaTime;
            movementTotal += movementHor;

            //debug info (mov dir)
            if (mov_dir == string.Empty) { mov_dir = m_input.hor_axis_val > 0.0F ? "RIGHT" : "LEFT"; }
            Debug.Log("moving " + mov_dir);
            mov_dir = string.Empty;
        }

        //check vertical movement;
        if (!Y_restraint && m_input.ver_axis_val != 0.0F)
        {
            Vector3 movementVer = transform.TransformDirection(Vector3.forward);
            movementVer *= m_input.ver_axis_val * climbingSpeed * Time.deltaTime;
            movementTotal += movementVer;

            //debug info (mov dir)
            if (mov_dir == string.Empty) { mov_dir = m_input.ver_axis_val > 0.0F ? "UP" : "DOWN"; }
            
            Debug.Log("moving " + mov_dir);
            mov_dir = string.Empty;
        }

        controller.Move(movementTotal);
    }

    /// <summary>
    /// interacting with other gameobjects when certain input is provided;
    /// </summary>
    private void Interaction()
    {
        if (m_input.START_pressed)
        {
            gm.StartPauseMenu();
        }

        if (m_input.SELECT_pressed)
        {
            gm.StartPauseMenu();
        }
    }
}