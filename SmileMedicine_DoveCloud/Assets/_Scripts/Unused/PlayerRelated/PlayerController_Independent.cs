﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Independent : MonoBehaviour
{
    public GameManager gm;
    public InputSystem m_input;
    public bool X_restraint;
    public bool Y_restraint;
    public bool Z_restraint;
    public bool enable_movement;

    public float runningSpeed;
    public float crouchingSpeed;
    public float climbingSpeed;
    public float z_movementUnit;
    public float gravityFactor = 0.98F;

    public float speedFactor;

    private CharacterController controller;
    private float curSpeed;
    private bool usingLadder;
    private string mov_dir;

    private bool startSpecialMove;
    private Vector3 des_pos;
    private Vector3 des_scale;
    private Vector3 total_scale_change;
    private float des_dis;
    private float switchSpeed;

    private float last_pos_y;
    private float last_delta_time;

    private bool inputLoackAcquired;
    public bool InputLockAcquired { get { return inputLoackAcquired; } set { } }

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
        Z_restraint = false;
        enable_movement = true;

        mov_dir = string.Empty;
        usingLadder = false;
        curSpeed = 0.0F;

        startSpecialMove = false;

        inputLoackAcquired = true;
        speedFactor = 1.0F;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputLoackAcquired)
        {
            if (startSpecialMove)
            {
                startSpecialMove = !PlayerSpecialMove(des_pos, switchSpeed);
            }
            else
            {
                PlayerMove();
            }
        }
    }

    public void ChangeInputLock()
    {
        inputLoackAcquired = !inputLoackAcquired;
    }

    public void StartSpecialMove(Vector3 des_pos, float switchSpeed, Vector3 des_scale)
    {
        startSpecialMove = true;
        this.des_pos = des_pos;
        this.switchSpeed = switchSpeed;
        des_dis = (des_pos - transform.position).magnitude;
        this.des_scale = des_scale;
        total_scale_change = des_scale - transform.localScale;
        speedFactor = des_scale.x;
    }

    private bool PlayerSpecialMove(Vector3 des_pos, float switchSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, des_pos, switchSpeed * Time.deltaTime);

        float scale_ratio_sub = (des_pos - transform.position).magnitude / des_dis;
        transform.localScale = des_scale - (total_scale_change * scale_ratio_sub);

        if (des_pos == transform.position)
        {
            Debug.Log("player arrive: " + des_pos);
        }

        return des_pos == transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("player impulseL: " + collision.impulse);
    }

    private float AddGravity(Vector3 input_mov)
    {
        float y_dif = last_pos_y - transform.position.y;
        float v_start = (y_dif / last_delta_time) + ((gravityFactor * last_delta_time) / 2);
        float v_end = v_start + (gravityFactor * Time.deltaTime);

        last_pos_y = transform.position.y;
        last_delta_time = Time.deltaTime;

        float ans = -((v_start + v_end) * Time.deltaTime) / 2;
        //Debug.Log("y_mov: " + v_end);

        return ans;
    }

    /// <summary>
    /// basic player movement in 2D space, horizontally and climbing;
    /// </summary>
    private void PlayerMove()
    {
        if (!enable_movement) { return; }

        Vector3 movementTotal = new Vector3(0.0F, 0.0F, 0.0F);

        //add gravity?

        curSpeed = m_input.B_hold ? crouchingSpeed : runningSpeed;
        curSpeed *= speedFactor;
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
            Vector3 movementVer = transform.TransformDirection(Vector3.up);
            movementVer *= m_input.ver_axis_val * climbingSpeed * Time.deltaTime;
            movementTotal += movementVer;

            //debug info (mov dir)
            if (mov_dir == string.Empty) { mov_dir = m_input.ver_axis_val > 0.0F ? "UP" : "DOWN"; }
            Debug.Log("moving " + mov_dir);
            mov_dir = string.Empty;
        }

        //check horizontal movement z_axis:
        if (!Z_restraint && m_input.ver_axis_val != 0.0F)
        {
            Vector3 movementHor = transform.TransformDirection(Vector3.forward);
            movementHor *= m_input.hor_axis_val * curSpeed * Time.deltaTime;
            movementTotal += movementHor;

            //debug info (mov dir)
            if (mov_dir == string.Empty) { mov_dir = m_input.hor_axis_val > 0.0F ? "FORWRARD" : "BACKWARD"; }
            Debug.Log("moving " + mov_dir);
            mov_dir = string.Empty;
        }

        //movementTotal += new Vector3(0.0F, AddGravity(movementTotal), 0.0F);
        //movementTotal += Vector3.down * Time.deltaTime;
        //Debug.Log("move_y: " + (Vector3.down * Time.deltaTime).y);
        controller.Move(movementTotal);
    }

    /*
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
    */
}