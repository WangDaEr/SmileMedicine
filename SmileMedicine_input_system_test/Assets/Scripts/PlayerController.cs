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

    public float runningSpeed;
    public float crochingSpeed;
    public float climbingSpeed;
    
    private CharacterController controller;
    private bool usingLadder;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        usingLadder = false;

        X_restraint = false;
        Y_restraint = true;  //initially the player can only move horizontally;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Interaction();
    }

    /// <summary>
    /// basic player movement in 2D space, horizontally and climbing;
    /// </summary>
    private void PlayerMove()
    {
        Vector3 movementTotal = new Vector3(0.0F, 0.0F, 0.0F);

        //add gravity?

        //check horizontal movement;
        if (!X_restraint && m_input.hor_axis_val != 0.0F)
        {
            Vector3 movementHor = transform.TransformDirection(Vector3.right);
            movementHor *= m_input.hor_axis_val * runningSpeed * Time.deltaTime;
            movementTotal += movementHor;
            //Debug.Log("movementHor" + m_input.hor_axis_val + " dir: hor");
        }

        //check vertical movement;
        if (!Y_restraint && m_input.ver_axis_val != 0.0F)
        {
            Vector3 movementVer = transform.TransformDirection(Vector3.forward);
            movementVer *= m_input.ver_axis_val * runningSpeed * Time.deltaTime;
            movementTotal += movementVer;
            //Debug.Log("movementHor" + m_input.ver_axis_val + " dir: ver");
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
