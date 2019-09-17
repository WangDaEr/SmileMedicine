using System.Collections;
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
    private bool z_restraint;
    public bool Z_restraint { get { return z_restraint; } set { z_restraint = value; Debug.Log("ZRestraint: " + z_restraint); } }
    public bool enable_movement;

    public float runningSpeed;
    public float crouchingSpeed;
    public float climbingSpeed;
    public float z_movementUnit;
    public float z_movementSpeed;
    public bool z_movementClip;
    public bool z_test;
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

    private bool specifyDestination;
    public bool SpecifyDestination { get { return specifyDestination; } set { } }
    private bool startZMove;
    private float usedZUnit;
    private Vector3 zMoveDestination;
    private Vector3 zMoveStartPosition;
    private Vector3 zMoveForwardPosition;
    private Vector3 zMoveForwardScale;
    private Vector3 zMoveBackPosition;
    private Vector3 zMoveBackScale;

    private float gravityTimer;

    public float fallLimit;

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
        Z_restraint = true;
        specifyDestination = false;
        enable_movement = true;

        mov_dir = string.Empty;
        usingLadder = false;
        curSpeed = 0.0F;

        startSpecialMove = false;
        startZMove = false;

        inputLoackAcquired = true;
        speedFactor = 1.0F;

        Debug.Log("local scale: " + transform.localScale);
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
            else if (startZMove)
            {
                startZMove = !ZMove();

                if (specifyDestination && (transform.position == zMoveDestination || transform.position == zMoveStartPosition))
                {
                    startZMove = false;
                    X_restraint = false;
                    Y_restraint = true;
                    Z_restraint = true;
                    specifyDestination = false;
                    Debug.Log("player arrive portal destination " + startZMove);
                }
            }
            else
            {
                PlayerMove();
            }
        }

        checkPlayerRespawn();
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

    public void StartZMove(float z_axis_val)
    {
        startZMove = true;

        usedZUnit = z_movementUnit;
        des_pos = transform.position + ((z_axis_val > 0.0F ? Vector3.forward : Vector3.back) * z_movementUnit);
        des_scale = new Vector3(1.0F, 1.0F, 1.0F);
        total_scale_change = des_scale - transform.localScale;
        des_dis = (des_pos - transform.position).magnitude;
    }

    public void StartZMove(float z_axis_val, Vector3 des_pos, Vector3 des_scale)
    {
        startZMove = true;

        usedZUnit = z_movementClip ?
            Mathf.Max(z_movementUnit, (des_pos - transform.position).magnitude) :
            z_movementUnit;
        this.des_pos = des_pos;

        Debug.Log("start z move des_pos: " + des_pos);

        this.des_scale = des_scale;
        total_scale_change = des_scale - transform.localScale;
        des_dis = (des_pos - transform.position).magnitude;
    }

    public void SetZAxisMovePara(Vector3 des_pos, Vector3 des_scale)
    {
        specifyDestination = true;
        zMoveDestination = des_pos;
        zMoveStartPosition = transform.position;

        if (des_pos.z > transform.position.z)
        {
            zMoveForwardPosition = des_pos;
            zMoveForwardScale = des_scale;
            zMoveBackPosition = transform.position;
            zMoveBackScale = transform.localScale;
        }
        else
        {
            zMoveForwardPosition = transform.position;
            zMoveForwardScale = transform.localScale;
            zMoveBackPosition = des_pos;
            zMoveBackScale = des_scale;
        }

        X_restraint = true;
        Y_restraint = true;
        Z_restraint = false;

        Debug.Log("set specifyDestination: " + specifyDestination);
    }

    private bool ZMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, des_pos, z_movementSpeed * Time.deltaTime);

        float scale_ratio_sub = (des_pos - transform.position).magnitude / des_dis;

        //Debug.Log("player local scale: " + transform.localScale + " " + total_scale_change + " " + scale_ratio_sub);

        transform.localScale = des_scale - (total_scale_change * scale_ratio_sub);

        if (des_pos == transform.position)
        {
            Debug.Log("player arrive: " + des_pos);
        }

        return des_pos == transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("player impulse: " + collision.impulse);
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

    public void StartZMove_OneStep(Vector3 des_scale)
    {
        Vector3 step_des = zMoveDestination;
        Vector3 step_pos = Vector3.MoveTowards(transform.position, step_des, z_movementUnit);

        Debug.Log("step_pos: " + step_pos);

        float scale_ratio = (step_pos - transform.position).magnitude / (step_des - transform.position).magnitude;
        //float scale_diff = ((m_input.ver_axis_val > 0.0F ? zMoveForwardScale.x : zMoveBackScale.x) - transform.localScale.x) * scale_ratio;
        float scale_diff = (des_scale.x - transform.localScale.x) * scale_ratio;
        StartZMove(m_input.ver_axis_val, step_pos, new Vector3(scale_diff, scale_diff, scale_diff) + transform.localScale);

        Debug.Log("des Scale: " + scale_diff + scale_ratio);
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
        //if (!X_restraint && m_input.hor_axis_button != InputSystem.AxisButtomPressed.NoInput)
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

        //if (!Z_restraint && m_input.ver_axis_val != 0.0F)
        if (!Z_restraint && m_input.ver_axis_button != InputSystem.AxisButtomPressed.NoInput)
        {
            if (specifyDestination)
            {
                Vector3 step_des = m_input.ver_axis_val > 0.0F ? zMoveForwardPosition : zMoveBackPosition;
                Vector3 step_pos = Vector3.MoveTowards(transform.position, step_des, z_movementUnit);

                Debug.Log("step_pos: " + step_pos);

                float scale_ratio = (step_pos - transform.position).magnitude / (step_des - transform.position).magnitude;
                float scale_diff = ((m_input.ver_axis_val > 0.0F ? zMoveForwardScale.x : zMoveBackScale.x) - transform.localScale.x) * scale_ratio;
                StartZMove(m_input.ver_axis_val, step_pos, new Vector3(scale_diff, scale_diff, scale_diff) + transform.localScale);
            }
            else if (z_test)
            {
                StartZMove(m_input.ver_axis_val);
                Debug.Log("test Z movement");
            }

            //debug info (mov dir)
            if (mov_dir == string.Empty) { mov_dir = m_input.ver_axis_val > 0.0F ? "FORWARD" : "BACK"; }
            Debug.Log("moving " + mov_dir);
            mov_dir = string.Empty;
        }

        //add gravity;
        if (Y_restraint && Z_restraint)
        {
            float mag = (2 * gravityTimer + Time.deltaTime) * Time.deltaTime / 2;
            movementTotal += Physics.gravity * mag;
        }
        if (controller.isGrounded)
        {
            gravityTimer = 0.0F;
        }
        else
        {
            gravityTimer += Time.deltaTime;
        }

        controller.Move(movementTotal);
    }

    private void checkPlayerRespawn()
    {
        if (transform.position.y < fallLimit && !controller.isGrounded)
        {
            gm.RespawnPlayerCharacter();
        }
    }
}
