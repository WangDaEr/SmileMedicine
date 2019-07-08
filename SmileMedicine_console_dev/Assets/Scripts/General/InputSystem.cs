using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// provided proccessed input value;
/// </summary>
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

    public AxisButtomPressed hor_axis_button;
    private bool hor_button_lock;
    public AxisButtomPressed ver_axis_button;
    private bool ver_button_lock;

    public enum AxisButtomPressed
    {
        Positive,
        Negative,
        NoInput
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ResetAllInputs();
    }

    // Update is called once per frame
    void Update()
    {
        ReadAllInputs();
        SetAxisButtonPressed(ref hor_axis_button, hor_axis_val, ref hor_button_lock);
        SetAxisButtonPressed(ref ver_axis_button, ver_axis_val, ref ver_button_lock);
        //Debug.Log("Change: " + hor_axis_button );
    }

    /// <summary>
    /// initialize the value;
    /// </summary>
    private void ResetAllInputs()
    {
        hor_axis_val = 0.0F;
        ver_axis_val = 0.0F;
        A_pressed = false;
        B_pressed = false;
        START_pressed = false;
        SELECT_pressed = false;

        hor_axis_button = AxisButtomPressed.NoInput;
        ver_axis_button = AxisButtomPressed.NoInput;
    }

    private void SetAxisButtonPressed(ref AxisButtomPressed button, float val, ref bool button_lock)
    {
        //Debug.Log("Change: " + button + " " + (button != AxisButtomPressed.Positive) + " " + (val > 0.0000000F));
        if (val > 0.0F && button != AxisButtomPressed.Positive && !button_lock)
        {
            //Debug.Log("Positive##################");
            button = AxisButtomPressed.Positive;
            button_lock = true;
        }
        else if (val < 0.0F && button != AxisButtomPressed.Negative && !button_lock)
        {
            button = AxisButtomPressed.Negative;
            button_lock = true;
        }
        else
        {
            
            button = AxisButtomPressed.NoInput;
            
        }

        if (val == 0.0F)
        {
            button_lock = false;
        }
        //Debug.Log("Change: " + hor_axis_button);
    }

    /// <summary>
    /// check and read raw inputs from default input stream;
    /// extension: location for code that reads input from switch in a specific way;
    /// </summary>
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
