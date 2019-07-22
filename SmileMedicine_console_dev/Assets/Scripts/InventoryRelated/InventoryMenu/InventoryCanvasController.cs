using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvasController : MonoBehaviour
{
    public GameManager gm;
    public InputSystem m_input;

    public int initialPanelIdx;
    public int initialButtonIdx;
    public GameObject initialButton;

    public int currentPanelIdx;
    public int currentButtonIdx;

    private bool inputLockAcquired;
    private Dictionary<InputSystem.AxisButtomPressed, int> inputMapping;

    // Start is called before the first frame update
    void Start()
    {
        inputLockAcquired = false;

        //inputMappingSetup();
        initializeLayout();
        Debug.Log("Canvas active;");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CanvasInputLock!!!!!: " + inputLockAcquired);
        
        //check input lock when has player controller
        /*
        if (inputLockAcquired)
        {
            CanvasInput();
        }
        */

        CanvasInput();
        Interaction();
    }

    private void inputMappingSetup()
    {
        inputMapping = new Dictionary<InputSystem.AxisButtomPressed, int>
        {
            { InputSystem.AxisButtomPressed.Positive, 1 },
            { InputSystem.AxisButtomPressed.Negative, -1 },
            { InputSystem.AxisButtomPressed.NoInput, 0 }
        };
    }

    private void initializeLayout()
    {
        currentButtonIdx = 0;
        currentPanelIdx = 1;
    }

    private void CanvasInput()
    {
        Debug.Log("Canvas: " + m_input.hor_axis_button);

        int newButtonIdx = -1;
        int newPanelIdx = -1;
        switch (m_input.hor_axis_button)
        {
            case InputSystem.AxisButtomPressed.Positive:

                newButtonIdx = currentButtonIdx + 1;
                newButtonIdx = newButtonIdx >= transform.GetChild(currentPanelIdx).childCount ? transform.GetChild(currentPanelIdx).childCount - 1 : newButtonIdx;

                break;

            case InputSystem.AxisButtomPressed.Negative:

                newButtonIdx = currentButtonIdx - 1;
                newButtonIdx = newButtonIdx < 0 ? 0 : newButtonIdx;

                break;

            default:
                break;
        }

        switch (m_input.ver_axis_button)
        {
            case InputSystem.AxisButtomPressed.Positive:

                newPanelIdx = currentPanelIdx + 1;
                newPanelIdx = newPanelIdx >= 2 ? 1 : newPanelIdx;

                newButtonIdx = newButtonIdx < 0 ? currentButtonIdx : newButtonIdx;
                newButtonIdx = newButtonIdx >= transform.GetChild(newPanelIdx).childCount ? transform.GetChild(newPanelIdx).childCount - 1 : newButtonIdx;

                break;

            case InputSystem.AxisButtomPressed.Negative:

                newPanelIdx = currentPanelIdx - 1;
                newPanelIdx = newPanelIdx < 0 ? 0 : newPanelIdx;

                newButtonIdx = newButtonIdx < 0 ? currentButtonIdx : newButtonIdx;
                newButtonIdx = newButtonIdx >= transform.GetChild(newPanelIdx).childCount ? transform.GetChild(newPanelIdx).childCount - 1 : newButtonIdx;

                break;

            default:
                break; 
        }

        if (newButtonIdx != -1)
        {
            newPanelIdx = newPanelIdx < 0 ? currentPanelIdx : newPanelIdx;

            transform.GetChild(currentPanelIdx).GetChild(currentButtonIdx).GetComponent<InventoryButton>().LoseFocus();
            transform.GetChild(newPanelIdx).GetChild(newButtonIdx).GetComponent<InventoryButton>().GetFocus();

            Debug.Log("new Button: " + newButtonIdx + " panel: " + newPanelIdx);

            currentButtonIdx = newButtonIdx;
            currentPanelIdx = newPanelIdx;
        }
    }

    private void Interaction()
    {
        if (m_input.A_pressed)
        {
            transform.GetChild(currentPanelIdx).GetChild(currentButtonIdx).GetComponent<InventoryButton>().ButtonClick();
        }
    }
}
