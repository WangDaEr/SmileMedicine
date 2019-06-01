using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public InputSystem m_input;
    public bool X_restraint;
    public bool Y_restraint;

    public float runningSpeed = 0;
    public float climbingSpeed = 0;
    
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

    private void Interaction()
    {

    }
}
