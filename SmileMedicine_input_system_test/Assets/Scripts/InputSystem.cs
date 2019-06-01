using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public float hor_axis_val;
    public float ver_axis_val;
    public bool A_pressed;
    public bool A_hold;
    public bool B_pressed;
    public bool B_hold;
    public bool START_pressed;
    public bool SELECT_pressed;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetAllInputs();
    }

    // Update is called once per frame
    void Update()
    {
        ReadAllInputs();
    }

    private void ResetAllInputs()
    {
        hor_axis_val = 0.0F;
        ver_axis_val = 0.0F;
        A_pressed = false;
        B_pressed = false;
        START_pressed = false;
        SELECT_pressed = false;
    }

    private void ReadAllInputs()
    {
        hor_axis_val = Input.GetAxis("Horizontal");
        ver_axis_val = Input.GetAxis("Vertical");
        A_pressed = Input.GetButtonDown("A");
        B_pressed = Input.GetButtonDown("B");
        A_hold = Input.GetButton("A");
        B_hold = Input.GetButton("B");
        START_pressed = Input.GetButtonDown("START");
        SELECT_pressed = Input.GetButtonDown("SELECT");
    }
}
